using Bookshop_Models.ViewModels;

namespace Bookshop_Business.Repository.IRepository;

public interface IBookRepository
{
    public Tuple<List<BookDTO>, int> GetAllBooks(DateTime fromdate, DateTime toDate,int pageId = 1, string bookTitle = "", string authorName = "", int categoryId = 0, double startPrice = 0, double endPrice = 0,int publisherId = 0);
    public Task<List<BookDTO>> GetLatestBooks();
    public Task<List<BookDTO>> GetAllBooksWithoutPagination();
    public Task<BookDTO> GetBookById(int BookId);
    //public Task<WishlistBookViewModel> GetBooksForWishlist(string userId);
    public string GetBookTitle(int bookId);
    public Task<List<BookDTO>> GetBookBySearchTxt(string q);
    public Task<List<int>> GetBookCategoryIds(int BookId);
    public Task<List<string>> GetbookCategoryTitle(int BookId);
    public Task<string> GetbookAuthorName(int BookId);
    public Task<List<BookDTO>> GetBooksByPublisherId(int PublisherId);
    public Task<List<BookDTO>> GetBooksByCategoryId(int categoryId);
    public Task<List<BookDTO>> GetBooksByAuthorId(int categoryId);
    public Task<BookDTO> AddBook(BookDTO BookDTO);
    public Task<BookDTO> UpdateBook(int BookId, BookDTO BookDTO);
    public Task<bool> DeleteBookById(int BookId);
    public Task<bool> DecreaseQuantity(int BookId);
    //public Task<bool> DoesBookHaveActiveDiscount(int BookId);

}
