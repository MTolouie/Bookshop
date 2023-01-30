
namespace Bookshop_Models.DTOs;

public class CartDetailsDTO
{
    public int DetailId { get; set; }

    [Required]
    public int CartId { get; set; }

    [Required]
    public int BookId { get; set; }

    [Required]
    public double Price { get; set; }

    public BookDTO Book { get; set; }
}
