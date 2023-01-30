
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshop_DataLayer.Models;

public class Address
{
    [Key]
    public int AddressId { get; set; }

    [Display(Name = "User")]
    [Required(ErrorMessage = "{0} User Is Required")]
    public string UserId { get; set; }

    [Display(Name = "Address Title")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string AddressTitle { get; set; }

    [Display(Name = "Province")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string PROVINCE { get; set; }

    [Display(Name = "Town")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string Town { get; set; }

    [Display(Name = "Zipcode")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string Zipcode { get; set; }

    [Display(Name = "Phone Number")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string PhoneNo { get; set; }

    [Display(Name = "Address")]
    [Required(ErrorMessage = "{0} Is Required")]
    public string AddressDescription { get; set; }

    [Display(Name = "Is Selected")]
    public bool IsSelected { get; set; } = false;

    #region Relations
    [ForeignKey("UserId")]
    public ApplicationUser User{ get; set; }

    public List<Cart> Carts{ get; set; }
    #endregion
}
