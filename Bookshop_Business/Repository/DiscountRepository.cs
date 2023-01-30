

namespace Bookshop_Business.Repository;

public class DiscountRepository : IDiscountRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public DiscountRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<bool> AddDiscount(DiscountDTO discountsDTO)
    {
        try
        {
            var discount = _mapper.Map<DiscountDTO,Discount>(discountsDTO);
            _db.Discounts.Add(discount);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteDiscountById(int discountId)
    {
        try
        {
            var discount = await _db.Discounts.FindAsync(discountId);
            if(discount is null)
                return false;
            _db.Discounts.Remove(discount);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<Tuple<List<DiscountDTO>, int>> GetAllDiscounts(int pageId = 1, int bookId= 0)
    {
        try
        {
            IQueryable<Discount> discounts = _db.Discounts;

            if (bookId != 0)
            {
                discounts = discounts.Where(d=>d.BookId == bookId && d.IsExpired ==false);
            }

            //if (DateTime.MinValue != fromdate)
            //{
            //    //DateTime FromDate = DateTime.ParseExact(fromdate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            //    books = books.Where(u => u.CreatedDate >= fromdate);
            //}

            //if (DateTime.MinValue != toDate)
            //{
            //    //DateTime ToDate = DateTime.ParseExact(toDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            //    books = books.Where(u => u.CreatedDate <= toDate);
            //}


            var discountDTO = _mapper.Map<IQueryable<Discount>, List<DiscountDTO>>(discounts);

            int take = 5;
            int skip = (pageId - 1) * take;
            var pageCount = discountDTO.Count() / take;
            if (pageCount % 2 != 0)
            {
                pageCount++;
            }


            var query = discountDTO.OrderByDescending(c => c.DiscountId).Skip(skip).Take(take).ToList();

            return Tuple.Create(query, pageCount);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<DiscountDTO> GetDiscountByBookId(int bookId)
    {
        try
        {
            var discount = await _db.Discounts.Where(d => d.BookId == bookId && d.IsExpired == false).SingleOrDefaultAsync();
            if(discount is null)
                return null;

            var discountDTO = _mapper.Map<Discount,DiscountDTO>(discount);
            return discountDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<DiscountDTO> GetDiscountById(int discountId)
    {
        try
        {
            var discount = await _db.Discounts.SingleOrDefaultAsync(d => d.DiscountId == discountId);
            if (discount is null)
                return null;

            var discountDTO = _mapper.Map<Discount,DiscountDTO>(discount);

            return discountDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> UpdateDiscount(int discountId, DiscountDTO discountsDTO)
    {
        try
        {
            if (discountId != discountsDTO.DiscountId)
                return false;

            var discount = await _db.Discounts.FindAsync(discountId);
            var convertedDiscount = _mapper.Map<DiscountDTO, Discount>(discountsDTO, discount);
            _db.Discounts.Update(convertedDiscount);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DoesDiscountExists(int bookId)
    {
        try
        {
            var discount = await _db.Discounts.Where(d => d.BookId == bookId && d.IsExpired == false).FirstOrDefaultAsync();
            if (discount is not null)
                return true;

            return false;
        }
        catch (Exception ex)
        {
            return true;
        }
    }

    public async Task<double> GetBookDiscountPercentage(int bookId)
    {
        try
        {
            var discountPercentage = await _db.Discounts
                .Where(c => c.BookId == bookId)
                .Select(d => d.DiscountPercent)
                .SingleOrDefaultAsync();

            if (discountPercentage is 0)
                return 0;

            return discountPercentage;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<List<DiscountDTO>> GetTwoLatestDiscounts()
    {
        try
        {
            var discounts = await _db.Discounts.Where(d=>d.endDate >= DateTime.Now && d.IsExpired == false).OrderByDescending(d => d.DiscountId).Take(2).ToListAsync();

            var discountsDTO = _mapper.Map<List<Discount>,List<DiscountDTO>>(discounts);
            return discountsDTO;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
