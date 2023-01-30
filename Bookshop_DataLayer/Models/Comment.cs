
namespace Bookshop_DataLayer.Models;

public class Comment
{
    [Key]
    public int CommentId { get; set; }

    [Display(Name ="Book")]
    [Required(ErrorMessage ="{0} Is Required")]
    public int BookId { get; set; }

    [Display(Name = "User")]
    [Required(ErrorMessage = "{0} Is Required")]
    public string UserId { get; set; }


    [Display(Name = "Rating")]
    [Required(ErrorMessage = "{0} Is Required")]
    public double Rating { get; set; }

    [Display(Name = "Comment")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(500,ErrorMessage ="{0} Should Not Be More Than 500 Characters")]
    public string CommentText { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.Now;

    #region Relations
    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }

    [ForeignKey("BookId")]
    public Book Book{ get; set; }
    #endregion
}
