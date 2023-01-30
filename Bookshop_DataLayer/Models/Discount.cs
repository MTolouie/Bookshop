
namespace Bookshop_DataLayer.Models;

public class Discount
{
    [Key]
    public int DiscountId { get; set; }
    public int BookId { get; set; }
    [Required]
    public int DiscountPercent { get; set; }

    public bool IsExpired { get; set; } = false;

    public DateTime? StartDate { get; set; }

    public DateTime? endDate { get; set; }

    #region Relations
    [ForeignKey("BookId")]
    public Book Book{ get; set; }
    #endregion
}
