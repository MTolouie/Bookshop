
namespace Bookshop_Business.Repository.IRepository;

public interface IBookCategoryRepository
{
    Task<bool> AddBookCategory(BookCategoriesDTO bookCategoriesDTO);
    Task<bool> DeleteBookCategoryByBookId(int bookId);
    Task<List<int>> GetBookIdsByCategoryId(int catId);
    Task<List<int>> GetCategoryIdsByBookId(int bookId);


}
