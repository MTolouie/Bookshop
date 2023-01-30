﻿using System.Web.Mvc;

namespace Bookshop_Models.DTOs;

public class BookDTO
{
    
    public int BookId { get; set; }

    //[Display(Name = "Category")]
    //[Required(ErrorMessage = "{0} Is Required")]
    //public int CategoryId { get; set; }


    [Display(Name = "Book Cover Fromat")]
    [Required(ErrorMessage = "{0} Is Required")]
    public int CoverFormatId { get; set; }

    [Display(Name = "Publisher")]
    [Required(ErrorMessage = "{0} Is Required")]
    public int PublisherId { get; set; }

    [Display(Name = "Author")]
    [Required(ErrorMessage = "{0} Is Required")]
    public int AuthorId { get; set; }

    [Display(Name = "Translator")]
    public int? TranslatorId { get; set; }

    [Display(Name = "Book Title")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string Title { get; set; }

    [Display(Name = "ISBN")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string ISBN { get; set; }

    [Display(Name = "Image")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(100)]
    public string Image { get; set; }

    [Display(Name = "Description")]
    [Required(ErrorMessage = "{0} is required")]
    [AllowHtml]
    public string Description { get; set; }

    [Display(Name = "Price")]
    [Required(ErrorMessage = "{0} Is Required")]
    public double Price { get; set; }

    [Display(Name = "Quantity")]
    [Required(ErrorMessage = "{0} Is Required")]
    public int Quantity { get; set; }

    [Display(Name = "Tags")]
    [Required(ErrorMessage = "{0} Is Required")]
    [MaxLength(150)]
    public string Tags { get; set; }

    [Display(Name = "Published Year")]
    [Required(ErrorMessage = "{0} Is Required")]
    public DateTime PublishedYear { get; set; }

    [Display(Name = "Created Date")]
    [Required(ErrorMessage = "{0} Is Required")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;

}
