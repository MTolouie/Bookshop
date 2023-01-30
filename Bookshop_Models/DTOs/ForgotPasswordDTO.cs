
namespace Bookshop_Models.DTOs;

public class ForgotPasswordDTO
{
    [EmailAddress]
    [Required(ErrorMessage ="Email Is Required")]
    public string Email { get; set; }
}
