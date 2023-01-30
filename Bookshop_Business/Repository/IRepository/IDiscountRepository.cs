using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop_Business.Repository.IRepository;

public interface IDiscountRepository
{
    public Task<Tuple<List<DiscountDTO>, int>> GetAllDiscounts(int pageId = 1, int bookId = 0);
    public Task<DiscountDTO> GetDiscountById(int discountId);
    public Task<List<DiscountDTO>> GetTwoLatestDiscounts();
    public Task<DiscountDTO> GetDiscountByBookId(int bookId);
    public Task<double> GetBookDiscountPercentage(int bookId);
    public Task<bool> AddDiscount(DiscountDTO discountsDTO);
    public Task<bool> UpdateDiscount(int discountId, DiscountDTO discountsDTO);
    public Task<bool> DeleteDiscountById(int discountId);
    public Task<bool> DoesDiscountExists(int bookId);
}
