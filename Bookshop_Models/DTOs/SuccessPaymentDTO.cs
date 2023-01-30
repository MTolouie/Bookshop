
namespace Bookshop_Models.DTOs;

public class SuccessPaymentDTO
{
    public string Title { get; set; }
    public int StatusCode { get; set; }
    public string SuccessMessage { get; set; }
    public object Data { get; set; }
}
