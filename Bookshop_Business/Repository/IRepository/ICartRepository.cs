
namespace Bookshop_Business.Repository.IRepository;

public interface ICartRepository
{
    public Task<bool> AddToCart(int bookId, string userId);
    public Task<CartDTO> GetUserCart(string userId);
    public Task<bool> DeleteFromCart(int cartId,int CartDetailId);
    public Task<bool> MarkCartIsFinally(int cartId);
    public Task<int> GetNumberOfUserCart(string userId);
    public Task<List<CartDTO>> GetUserRecentOrders(string userId);
}
