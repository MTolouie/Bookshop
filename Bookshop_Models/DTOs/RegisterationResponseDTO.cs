
namespace Bookshop_Models.DTOs;

public class RegisterationResponseDTO
{
    public bool IsRegisterationSuccessful { get; set; }
    public string Message { get; set; }
    public IEnumerable<string> Errors { get; set; }
}
