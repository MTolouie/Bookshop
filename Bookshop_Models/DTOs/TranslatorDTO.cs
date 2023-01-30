
namespace Bookshop_Models.DTOs;

public class TranslatorDTO
{
    public int TranslatorId { get; set; }

    [Display(Name = "Translator Name")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string TranslatorName { get; set; }

}
