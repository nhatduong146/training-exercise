using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfullAPI.Exceptions;
using RESTfullAPI.Extensions;
using RESTfullAPI.Infrastructure.Configurations;
using RESTfullAPI.Infrastructure.DbContexts;
using RESTfullAPI.Middlewares;
using RESTfullAPI.Services.ProductService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();

// Add database context
var msSqlConnectionString = builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddDbContext<AppDbContext>(opt => opt
    .UseSqlServer(msSqlConnectionString, options =>
    {
        options.EnableRetryOnFailure();
    }));

// Register application service
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        var response = new JsonErrorResponse("Input is invalid", errors);
        return new BadRequestObjectResult(response);
    };
});

// Register rate limiter
builder.Services.RegisterRateLimiter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

app.Run();
