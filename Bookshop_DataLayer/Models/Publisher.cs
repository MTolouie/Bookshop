
namespace Bookshop_DataLayer.Models;

public class Publisher
{
    [Key]
    public int PublisherId { get; set; }

    [Display(Name = "Publisher TItle")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string PublisherTitle { get; set; }

    #region Relations
    public List<Book> Books { get; set; }
    #endregion
}
