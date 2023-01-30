
namespace Bookshop_Models.DTOs;

public class BookCoverFormatDTO
{
    public int CoverFormatId { get; set; }

    [Display(Name = "Cover Format")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string CoverFormatTitle { get; set; }
}
