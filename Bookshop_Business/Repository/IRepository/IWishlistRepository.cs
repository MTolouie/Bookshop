
namespace Bookshop_Business.Repository.IRepository;

public interface IWishlistRepository
{
    public Task<Tuple<List<WishlistDTO>, int>> GetAllUserWishlist(string userId, int pageId = 1);
    public Task<WishlistDTO> GetWishlistById(int wishlistId);
    public Task<WishlistDTO> GetWishListsByBookId(int bookId);
    public Task<bool> AddWishlist(WishlistDTO wishlistDTO);
    public Task<bool> UpdateWishlist(int wishlistId, WishlistDTO wishlistDTO);
    public Task<bool> DeleteWishlistById(int wishlistId);
    public Task<bool> DoesBookExistsInWishlist(string userId,int bookId);
    public Task<int> GetNumberOfUserWishlist(string userId);
}
