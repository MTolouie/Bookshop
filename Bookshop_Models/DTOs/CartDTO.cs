
namespace Bookshop_Models.DTOs;

public class CartDTO
{
    public int CartId { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    public int AddressId { get; set; }

    [Required]
    public double CartSum { get; set; }

    public bool IsFinally { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    public List<CartDetailsDTO> CartDetails { get; set; }
}
