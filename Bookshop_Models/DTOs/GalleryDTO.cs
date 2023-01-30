
namespace Bookshop_Models.DTOs;

public class GalleryDTO
{
    public int GalleryID { get; set; }
    public int BookId { get; set; }


    [Display(Name = "Image Title")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string Title { get; set; }

    [Display(Name = "Image")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(200)]
    public string Image { get; set; }
}
