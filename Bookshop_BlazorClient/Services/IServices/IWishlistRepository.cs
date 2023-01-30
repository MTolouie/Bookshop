namespace Bookshop_BlazorClient.Services.IServices
{
    public interface IWishlistRepository : IRepository<WishlistDTO>
    {
        public Task<Tuple<IEnumerable<WishlistDTO>, int>> GetAllUserWishlists(string url,string userId,string? token,int pageId = 1);
        public Task<int> GetNumberOfUserWishlist(string url,string action,string userId,string? token);
    }
}
