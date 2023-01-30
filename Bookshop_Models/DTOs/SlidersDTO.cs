
using System.Web.Mvc;

namespace Bookshop_Models.DTOs;

public class SlidersDTO
{
    public int SliderId { get; set; }

    [Display(Name = "Slider Title")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(50)]
    public string SliderTitle { get; set; }

    [Display(Name = "Slider Description")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(500)]
    [AllowHtml]
    public string ShortDesc { get; set; }

    [Display(Name = "Url")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(200)]
    public string Url { get; set; }

    [Display(Name = "Image")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string Image { get; set; }

    public DateTime StartDate { get; set; } = DateTime.Now;

    public DateTime EndDate { get; set; }
}
