
namespace Bookshop_Business.Repository;

public class BookCategoryRepository : IBookCategoryRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;

    public BookCategoryRepository(ApplicationDbContext db, IMapper mapper, IBookRepository bookRepository)
    {
        _db = db;
        _mapper = mapper;
        _bookRepository = bookRepository;
    }



    public async Task<bool> AddBookCategory(BookCategoriesDTO bookCategoriesDTO)
    {
        try
        {
            if (bookCategoriesDTO is null)
                return false;

            var boookCategory = _mapper.Map<BookCategoriesDTO, BookCategories>(bookCategoriesDTO);
            _db.BookCategories.Add(boookCategory);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteBookCategoryByBookId(int bookId)
    {
        try
        {

            var bookCategories = await _db.BookCategories.Where(c => c.BookId == bookId).ToListAsync();

            if (bookCategories.Count >= 0)
            {
                foreach (var item in bookCategories)
                {
                    _db.BookCategories.Remove(item);
                }
            }
            await _db.SaveChangesAsync();
            return true;


        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Task<List<int>> GetBookIdsByCategoryId(int catId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<int>> GetCategoryIdsByBookId(int bookId)
    {
        try
        {
            var categoryIds = await _db.BookCategories.Where(b => b.BookId == bookId).Select(b => b.CatId).ToListAsync();
            if (categoryIds.Count == 0)
                return null;

            return categoryIds;
        }
        catch (Exception ex)
        {
            return null;
        }
    }


}
