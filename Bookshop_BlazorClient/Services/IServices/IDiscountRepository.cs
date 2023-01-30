namespace Bookshop_BlazorClient.Services.IServices;

public interface IDiscountRepository : IRepository<DiscountDTO>
{
    Task<DiscountDTO> GetBookDiscount(string url, string action, int bookId);
    Task<List<DiscountDTO>> GetTwoLatestDiscounts(string url, string action);
}
