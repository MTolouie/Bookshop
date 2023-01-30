
namespace Bookshop_DataLayer.Models;

public class Cart
{
    [Key]
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


    #region relations

    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }

    [ForeignKey("AddressId")]
    public Address Address{ get; set; }

    public List<CartDetail> CartDetails { get; set; }
    #endregion

}
