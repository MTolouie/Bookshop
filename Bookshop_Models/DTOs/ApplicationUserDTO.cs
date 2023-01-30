
using Bookshop_DataLayer.Models;

namespace Bookshop_Models.DTOs;

public class ApplicationUserDTO : ApplicationUser
{
    [Display(Name = "Full Name")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string FullName { get; set; }
}
