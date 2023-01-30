
namespace Bookshop_Models.DTOs;

public class UserRequestDTO
{
    [Required(ErrorMessage = "Name is required")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone Number is required")]
    public string PhoneNo { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and confirm password is not matched")]
    public string ConfirmPassword { get; set; }
}
