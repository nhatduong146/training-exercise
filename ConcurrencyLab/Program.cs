using ConcurrencyLab.Infrastructure.DbContexts;
using ConcurrencyLab.Infrastructure.Services.OrderQueue;
using ConcurrencyLab.Infrastructure.Services.PaymentGateway;
using ConcurrencyLab.Infrastructure.Worker;
using ConcurrencyLab.Services.OrderService;
using ConcurrencyLab.Services.PaymentService;
using ConcurrencyLab.Services.ProductService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database context
var msSqlConnectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(
    msSqlConnectionString, options =>
    {
        options.EnableRetryOnFailure();
    }));

// Choose 1 of 4 ways to config PaymentGatewayClient
// Add HttpClient
builder.Services.AddHttpClient<IPaymentGatewayClient, PaymentGatewayClient>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("https://67d581e6d2c7857431f09b90.mockapi.io/");
    })
    .SetHandlerLifetime(TimeSpan.FromSeconds(4)); // Default 2 mins


// Add HttpClient
builder.Services.AddHttpClient<IPaymentGatewayClient, PaymentGatewayClient>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("https://67d581e6d2c7857431f09b90.mockapi.io/");
    })
    .SetHandlerLifetime(Timeout.InfiniteTimeSpan)
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new SocketsHttpHandler
        {
            MaxConnectionsPerServer = 10, // Limit concurrent connections per server
            PooledConnectionLifetime = TimeSpan.FromMinutes(4), // Set lifetime (Same purpose of SetHandlerLifetime)
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2) // Set idle timeout
        };
        return handler;
    });


// Add HttpClient with ResilienceHandler
builder.Services.AddHttpClient<IPaymentGatewayClient, PaymentGatewayClient>()
    .AddResilienceHandler("CustomResilience", configure =>
    {
        // 1. Rate limiter
        configure.AddRateLimiter(new FixedWindowRateLimiter(new FixedWindowRateLimiterOptions()
        {
            PermitLimit = 100,
            Window = TimeSpan.FromSeconds(1)
        }));

        // 2. Total timeout
        configure.AddTimeout(new HttpTimeoutStrategyOptions
        {
            Timeout = TimeSpan.FromSeconds(30) // Total timeout for the request pipeline
        });

        // 3. Retry policy
        configure.AddRetry(new HttpRetryStrategyOptions
        {
            MaxRetryAttempts = 3, // Max retries
            Delay = TimeSpan.FromSeconds(2), // Initial delay
            BackoffType = DelayBackoffType.Exponential, // Exponential backoff
            UseJitter = true // Randomized delays to avoid retry storms
        });

        // 4. Circuit breaker
        configure.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
        {
            FailureRatio = 0.1, // 10% failure rate triggers circuit break
            MinimumThroughput = 100, // At least 100 requests before circuit evaluation
            SamplingDuration = TimeSpan.FromSeconds(30), // 30 seconds window
            BreakDuration = TimeSpan.FromSeconds(5) // 5 seconds before retrying
        });

        // 5. Attempt timeout
        configure.AddTimeout(new HttpTimeoutStrategyOptions
        {
            Timeout = TimeSpan.FromSeconds(10) // Individual request timeout
        });
    });


// Add HttpClient with default StandardResilienceHandler
builder.Services.AddHttpClient<IPaymentGatewayClient, PaymentGatewayClient>()
    .AddStandardResilienceHandler()
    .Configure(options =>
    {
        options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(5);

        options.Retry.MaxRetryAttempts = 5;

        options.CircuitBreaker.FailureRatio = 0.9;
        options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(5);
    });


// Final
builder.Services.AddHttpClient<IPaymentGatewayClient, PaymentGatewayClient>()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("https://67d581e6d2c7857431f09b90.mockapi.io/");
    })
    .AddStandardResilienceHandler();



// Add application services.
builder.Services.AddSingleton<OrderQueue>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

//builder.Services.AddScoped<IPaymentService, PaymentServiceV1>();
//builder.Services.AddScoped<IPaymentService, PaymentServiceV2>();
builder.Services.AddScoped<IPaymentService, PaymentServiceV3>();

builder.Services.AddHostedService<OrderProcessingWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
