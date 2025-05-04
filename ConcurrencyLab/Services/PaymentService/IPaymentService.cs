namespace ConcurrencyLab.Services.PaymentService
{
    public interface IPaymentService
    {
        Task ProcessOrderPaymentAsync(CancellationToken cancellationToken);
    }
}
