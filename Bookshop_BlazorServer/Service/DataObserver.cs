
using Bookshop_DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace Bookshop_BlazorServer.Service;

public static class DataObserver
{
    public static async Task DiscountDateObserver(IServiceProvider serviceProvider)
    {
        using (var _db = new ApplicationDbContext(
               serviceProvider.GetRequiredService<
                   DbContextOptions<ApplicationDbContext>>()))
        {
            if (_db.Discounts.Any())
            {
                var _discounts = await _db.Discounts.Where(d => d.IsExpired == false).ToListAsync();
                foreach (var discount in _discounts)
                {
                    if (discount.endDate < DateTime.Now)
                    {
                        discount.IsExpired = true;
                        _db.Discounts.Update(discount);
                        await _db.SaveChangesAsync();
                    }
                }
            }
            else
            {
                return;
            }

        }
    }
}
