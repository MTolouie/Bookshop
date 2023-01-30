
namespace Bookshop_Models.ViewModels;

public class ChangeAddressStatusViewModel
{

    public int addressId { get; set; }

    [Display(Name = "Status")]
    [Required(ErrorMessage ="{0} Is Required")]
    public bool Status { get; set; }
}
