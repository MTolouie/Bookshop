
namespace Bookshop_Business.Repository;

public class WishlistRepository : IWishlistRepository
{

    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public WishlistRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<bool> AddWishlist(WishlistDTO wishlistDTO)
    {
        try
        {
            var wishlist = _mapper.Map<WishlistDTO,Wishlist>(wishlistDTO);

            _db.Wishlists.Add(wishlist);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteWishlistById(int wishlistId)
    {
        try
        {
            var wishlist = await _db.Wishlists.FindAsync(wishlistId);
            if (wishlist is null)
                return false;

            _db.Wishlists.Remove(wishlist);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DoesBookExistsInWishlist(string userId, int bookId)
    {
        try
        {
            var wishlist = await _db.Wishlists.SingleOrDefaultAsync(w => w.BookId == bookId && w.UserId == userId);
            if (wishlist is null)
                return false;

            return true;
        }
        catch (Exception ex)
        {
            return true;
        }
    }

    public async Task<Tuple<List<WishlistDTO>, int>> GetAllUserWishlist(string userId, int pageId = 1)
    {
        IQueryable<Wishlist> wishlist = _db.Wishlists.Where(w=>w.UserId == userId);

        var wishlistDTO = _mapper.Map<IQueryable<Wishlist>, List<WishlistDTO>>(wishlist);

        int take = 5;
        int skip = (pageId - 1) * take;
        var pageCount = wishlistDTO.Count() / take;
        //if (pageCount % 2 != 0)
        //{
        //    pageCount++;
        //}
        var query = wishlistDTO.OrderByDescending(a => a.CreateDate).Skip(skip).Take(take).ToList();
        return Tuple.Create(query, pageCount);
    }

    public async Task<int> GetNumberOfUserWishlist(string userId)
    {
        try
        {
            var wishlists = await _db.Wishlists.Where(w => w.UserId == userId).ToListAsync();

            return wishlists.Count;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<WishlistDTO> GetWishlistById(int wishlistId)
    {
        try
        {
            var wishlist = await _db.Wishlists.FindAsync(wishlistId);

            if (wishlist is null)
                return null;

            var wishlistDTO = _mapper.Map<Wishlist,WishlistDTO>(wishlist);

            return wishlistDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<WishlistDTO> GetWishListsByBookId(int bookId)
    {
        try
        {
            var wishlist = await _db.Wishlists.SingleOrDefaultAsync(w=>w.BookId == bookId);

            if (wishlist is null)
                return null;

            var wishlistDTO = _mapper.Map<Wishlist, WishlistDTO>(wishlist);

            return wishlistDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> UpdateWishlist(int wishlistId, WishlistDTO wishlistDTO)
    {
        try
        {
            if (wishlistId != wishlistDTO.WishlistId)
                return false;


            var wishlist = await _db.Wishlists.FindAsync(wishlistId);
            if (wishlist is null)
                return false;

            var UpdatedWishlist = _mapper.Map<WishlistDTO, Wishlist>(wishlistDTO, wishlist);

            _db.Wishlists.Update(UpdatedWishlist);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
