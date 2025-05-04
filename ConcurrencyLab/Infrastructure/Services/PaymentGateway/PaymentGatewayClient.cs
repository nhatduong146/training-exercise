using ConcurrencyLab.Domain.Entities;
using Polly.CircuitBreaker;
using Polly.RateLimiting;

namespace ConcurrencyLab.Infrastructure.Services.PaymentGateway
{
    public class PaymentGatewayClient : IPaymentGatewayClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PaymentGatewayClient> _logger;

        public PaymentGatewayClient(HttpClient httpClient, ILogger<PaymentGatewayClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task ExecuteOrderPaymentAsync(IReadOnlyCollection<Order> orders, CancellationToken cancellationToken)
        {
            await Parallel.ForEachAsync(orders, cancellationToken, async (order, ct) =>
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync($"/orders/payments", order, ct);
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine($"{await response.Content.ReadAsStringAsync(ct)}");
                }
                catch (RateLimiterRejectedException ex)
                {
                    _logger.LogWarning("Payment request rejected by rate limiter: {Message}", ex.Message);
                    // Optionally implement a fallback mechanism or retry later
                }
                catch (BrokenCircuitException ex)
                {
                    _logger.LogError("Circuit breaker is open for payment gateway: {Message}", ex.Message);
                    // Implement a fallback mechanism or notify administrators
                }
                catch (TaskCanceledException ex)
                {
                    _logger.LogError("Payment request timed out: {Message}", ex.Message);
                    // Handle timeout scenarios
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError("Error communicating with payment gateway: {Message}", ex.Message);
                    // Log details about the HTTP error
                }
                catch (OperationCanceledException ex)
                {
                    _logger.LogInformation("Payment operation was cancelled: {Message}", ex.Message);
                    // Handle cancellation scenarios
                }
                catch (Exception ex) // Catch any other unexpected exceptions
                {
                    _logger.LogError("An unexpected error occurred during payment processing: {Message}", ex.Message);
                    // Log the unexpected error and potentially escalate
                }
            });
        }
    }
}
