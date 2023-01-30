namespace Bookshop_BlazorClient.Services.IServices;

public interface ICartRepository : IRepository<CartDTO>
{
    public Task<bool> AddToCart(string url,string action,int bookId,string userId, string token);
    public Task<CartDTO> GetUserCart(string url,string action,string userId, string token);
    public Task<bool> DeleteFromCart(string url,int cartId,int cartDetailId, string token);
    public Task<bool> MarkCartIsFinally(string url,int cartId,string StripeSessionId, string token);
    public Task<int> GetNumberOfUserCart(string url,string userId, string token);
    public Task<IEnumerable<CartDTO>> GetUserRecentOrders(string url,string action,string userId, string token);
}
