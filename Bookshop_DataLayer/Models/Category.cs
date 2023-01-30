
namespace Bookshop_DataLayer.Models;

public class Category
{
    [Key]
    public int catId { get; set; }

    [Display(Name = "Category Title")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string CatTitle { get; set; }

    #region Relations
    public List<BookCategories> BookCategories{ get; set; }
    #endregion
}
