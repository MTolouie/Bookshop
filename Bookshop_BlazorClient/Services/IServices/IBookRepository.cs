using Bookshop_Models.ViewModels;

namespace Bookshop_BlazorClient.Services.IServices;

public interface IBookRepository : IRepository<BookDTO>
{
    Task<List<BookDTO>> GetBooksByCategoryId(string url,string action,int categoryId);
    Task<Tuple<List<BookDTO>, int>> GetBooksWithPagination(string url,string action, int pageId = 0, int categoryId = 0, double startPrice = 0, double endPrice = 0, int publisherId = 0);
    Task<List<string>> GetbookCategoryTitle(string url,string action,int bookId);
    Task<string> GetbookAuthorName(string url,string action,int bookId);
    Task<List<BookDTO>> GetLatestBooks(string url,string action);
    Task<bool> DecreaseQuantity(string url,string action,int bookId,string token);
    Task<IEnumerable<BookDTO>> GetBookBySearchTxt(string url,string action,string q);
    Task<IEnumerable<BookDTO>> GetBooksByAuthorId(string url,string action,int authorId);
}
