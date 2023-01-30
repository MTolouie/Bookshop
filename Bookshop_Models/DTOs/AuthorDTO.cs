using System.Web.Mvc;

namespace Bookshop_Models.DTOs;

public class AuthorDTO
{
    public int AuthorID { get; set; }

    [Display(Name = "Author Name")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string AuthorName { get; set; }

    [Display(Name = "Author Biography")]
    //[Required(ErrorMessage = "{0} Is Required")]
    [AllowHtml]
    public string Biography { get; set; }

    [Display(Name = "Author Image")]
    //[Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string Image { get; set; }
}
