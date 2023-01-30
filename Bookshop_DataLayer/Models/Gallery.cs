
namespace Bookshop_DataLayer.Models;

public class Gallery
{
    [Key]
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


    #region Relations
    [ForeignKey("BookId")]
    public Book Book{ get; set; }
    #endregion
}
