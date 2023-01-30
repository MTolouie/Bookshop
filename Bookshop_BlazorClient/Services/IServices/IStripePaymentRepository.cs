namespace Bookshop_BlazorClient.Services.IServices;

public interface IStripePaymentRepository
{
    public Task<SuccessPaymentDTO> Checkout(string url, string action, StripePaymentDTO paymentDTO, string token);
}
