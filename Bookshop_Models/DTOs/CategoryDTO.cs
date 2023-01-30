
namespace Bookshop_Models.DTOs;

public class CategoryDTO
{
    public int catId { get; set; }

    [Display(Name = "Category Title")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string CatTitle { get; set; }
}
