
namespace Bookshop_Models.DTOs;

public class DiscountDTO
{
    public int DiscountId { get; set; }
    public int BookId { get; set; }

    [Display(Name ="Discount Percentage")]
    [Required(ErrorMessage ="{0} Is Required")]
    public int DiscountPercent { get; set; }

    public bool IsExpired { get; set; } = false;

    public DateTime StartDate { get; set; } = DateTime.Now;

    public DateTime endDate { get; set; } = DateTime.Now;
}
