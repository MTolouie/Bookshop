
namespace Bookshop_DataLayer.Models;

public class BookCategories
{
    [Key]
    public int BKId { get; set; }
    public int CatId { get; set; }
    public int BookId { get; set; }

    #region Relations
    [ForeignKey("BookId")]
    public Book Book{ get; set; }

    [ForeignKey("CatId")]
    public Category Category{ get; set; }
    #endregion


}
