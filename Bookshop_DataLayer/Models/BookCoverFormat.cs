
namespace Bookshop_DataLayer.Models;

public class BookCoverFormat
{
    [Key]
    public int CoverFormatId { get; set; }

    [Display(Name = "Cover Format")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string CoverFormatTitle { get; set; }


    #region Relations
    public List<Book> Books{ get; set; }
    #endregion
}
