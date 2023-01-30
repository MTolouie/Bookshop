using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop_Models.DTOs;

public class WishlistDTO
{
    public int WishlistId { get; set; }

    [Display(Name = "User")]
    [Required(ErrorMessage = "{0} Is Required")]
    public string UserId { get; set; }

    [Display(Name = "Book Id")]
    [Required(ErrorMessage = "{0} Is Required")]
    public int BookId { get; set; }


    [Display(Name = "Book Title")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string BookTitle { get; set; }

    [Display(Name = "Image")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string Image { get; set; }

    [Display(Name = "Price")]
    [Required(ErrorMessage = "{0} Is Required")]
    public double Price { get; set; }

    public bool IsDiscounted { get; set; }

    [Display(Name = "Discounted Price")]
    [Required(ErrorMessage = "{0} Is Required")]
    public double DiscountedPrice { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
}
