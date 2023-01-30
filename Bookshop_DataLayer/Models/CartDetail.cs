
namespace Bookshop_DataLayer.Models;

public class CartDetail
{
    [Key]
    public int DetailId { get; set; }

    [Required]
    public int CartId { get; set; }

    [Required]
    public int BookId { get; set; }

    [Required]
    public double Price { get; set; }

    #region relations

    [ForeignKey("CartId")]
    public Cart Cart { get; set; }
    
    [ForeignKey("BookId")]
    public Book Book { get; set; }    
    #endregion
}
