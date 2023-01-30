using Bookshop_Models.DTOs;

namespace Bookshop_Models.ViewModels;

public class BookFilterViewModel
{
    public string Name { get; set; } = "";

    public DateTime FromDate { get; set; } = DateTime.MinValue;

    public DateTime ToDate { get; set; } = DateTime.Now;

    public CategoryDTO SelectedCategory { get; set; }

    public AuthorDTO SelectedAuthor { get; set; }
}
