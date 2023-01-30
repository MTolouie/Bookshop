
using Microsoft.AspNetCore.Identity;

namespace Bookshop_DataLayer.Models;

public class ApplicationUser  : IdentityUser
{
    [Display(Name ="Full Name")]
    [Required(ErrorMessage ="{0} Is Required")]
    [MaxLength(100)]
    public string FullName { get; set; }

    public bool IsActive { get; set; } = true;

    #region Relations
    public List<Address> Addresses{ get; set; }
    public List<Comment> Comments{ get; set; }
    public List<Wishlist> Wishlists { get; set; }
    public List<Cart> Carts { get; set; }
    #endregion
}
