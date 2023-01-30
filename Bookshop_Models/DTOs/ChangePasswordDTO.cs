
namespace Bookshop_Models.DTOs;

public class ChangePasswordDTO
{
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
    public string Email{ get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "New Password is required.")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Confirm New password is required")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "New Password and confirm password is not matched")]
    public string ConfirmNewPassword { get; set; }

}
