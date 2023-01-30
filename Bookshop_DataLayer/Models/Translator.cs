
namespace Bookshop_DataLayer.Models;

public class Translator
{
    [Key]
    public int TranslatorId { get; set; }

    [Display(Name = "Translator Name")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string TranslatorName { get; set; }

    #region
    public List<Book> Books { get; set; }
    #endregion
}
