
namespace Bookshop_Models.DTOs;

public class PublisherDTO
{
    public int PublisherId { get; set; }

    [Display(Name = "Publisher TItle")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string PublisherTitle { get; set; }
}
