
using Bookshop_Business.Utilities;

namespace Bookshop_Business.Repository;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CartRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<bool> AddToCart(int bookId, string userId)
    {
        try
        {
            var cart = await _db.Cart
                .Where(c => c.UserId == userId && c.IsFinally == false)
                .SingleOrDefaultAsync();

            var addressId = await _db.Addresses
                .Where(a => a.UserId == userId && a.IsSelected == true).Select(s => s.AddressId)
                .SingleOrDefaultAsync();

            var book = await _db.Books.FindAsync(bookId);

            if (book is null)
                return false;

            var discount = await _db.Discounts
                .Where(d => d.BookId == book.BookId && d.IsExpired == false)
                .SingleOrDefaultAsync();

            var discountedPrice = book.Price;
            if (discount is not null)
            {
                discountedPrice = DiscountCalculator.CalculateDiscount(book.Price, discount.DiscountPercent);
            }

            if (addressId == 0)
                return false;

            if (cart is null)
            {
                cart = new()
                {
                    AddressId = addressId,
                    CreateDate = DateTime.Now,
                    IsFinally = false,
                    CartSum = discountedPrice,
                    UserId = userId
                };

                await _db.Cart.AddAsync(cart);
                await _db.SaveChangesAsync();

                CartDetail detials = new()
                {
                    BookId = book.BookId,
                    CartId = cart.CartId,
                    Price = discountedPrice,
                };

                await _db.CartDetail.AddAsync(detials);
                await _db.SaveChangesAsync();
            }
            else
            {
                var cartDetails = await _db.CartDetail.Where(c => c.CartId == cart.CartId && c.BookId == bookId).SingleOrDefaultAsync();

                if (cartDetails is null)
                {
                    CartDetail detials = new()
                    {
                        BookId = book.BookId,
                        CartId = cart.CartId,
                        Price = discountedPrice
                    };

                    await _db.CartDetail.AddAsync(detials);
                    await _db.SaveChangesAsync();

                    await UpdateCartPrice(cart.CartId);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private async Task UpdateCartPrice(int CartId)
    {
        var cart = await _db.Cart.FindAsync(CartId);
        cart.CartSum = _db.CartDetail.Where(d => d.CartId == CartId).Sum(s => s.Price);
        _db.Cart.Update(cart);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> DeleteFromCart(int cartId,int CartDetailId)
    {
        try
        {
            var cartDetial = await _db.CartDetail.FindAsync(CartDetailId);

            if (cartDetial is null)
                return false;

            _db.CartDetail.Remove(cartDetial);
            await _db.SaveChangesAsync();

            var cartDetails = await _db.CartDetail.Where(d => d.CartId == cartId).ToListAsync();

            if(cartDetails.Count == 0)
            {
                var cart = await _db.Cart.FindAsync(cartId);
                if(cart is not null)
                {
                    _db.Cart.Remove(cart);
                    await _db.SaveChangesAsync();
                }
                
            }
            else
            {
                await UpdateCartPrice(cartId);
            }
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<CartDTO> GetUserCart(string userId)
    {
        try
        {
            var cart = await _db.Cart
                .Include(c=>c.CartDetails)
                .ThenInclude(d=>d.Book)
                .Where(c=>c.IsFinally == false && c.UserId == userId)
                .SingleOrDefaultAsync();

            if(cart is null)
                return null;

            var cartDTO = _mapper.Map<Cart,CartDTO>(cart);

            return cartDTO;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> MarkCartIsFinally(int cartId)
    {
        try
        {
            var cart = await _db.Cart.FindAsync(cartId);

            if (cart is null)
                return false;

            cart.IsFinally = true;

            _db.Cart.Update(cart);
            await _db.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<int> GetNumberOfUserCart(string userId)
    {
        try
        {
            var carts = await _db.Cart.Where(c => c.UserId == userId).ToListAsync();

            return carts.Count;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<List<CartDTO>> GetUserRecentOrders(string userId)
    {
        try
        {
            var carts = await _db.Cart
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreateDate)
                .ToListAsync();

            if (carts is null)
                return null;

            var cartsDTO = _mapper.Map<List<Cart>,List<CartDTO>>(carts);

            return cartsDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
