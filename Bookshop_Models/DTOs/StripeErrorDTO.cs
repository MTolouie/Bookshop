
namespace Bookshop_Models.DTOs;

public class StripeErrorDTO
{
    public string Title { get; set; }
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }
}
