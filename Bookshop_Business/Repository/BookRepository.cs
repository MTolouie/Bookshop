

using Bookshop_Models.ViewModels;
using System.Globalization;

namespace Bookshop_Business.Repository;

public class BookRepository : IBookRepository
{

    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICategoryRepository _categoryRepository;

    public BookRepository(ApplicationDbContext db, IMapper mapper, IAuthorRepository authorRepository, ICategoryRepository categoryRepository)
    {
        _db = db;
        _mapper = mapper;
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<BookDTO> AddBook(BookDTO BookDTO)
    {
        try
        {
            if (BookDTO is null)
                return null;

            var Book = _mapper.Map<BookDTO, Book>(BookDTO);
            _db.Books.Add(Book);
            await _db.SaveChangesAsync();
            return _mapper.Map<Book, BookDTO>(Book);

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> DeleteBookById(int BookId)
    {
        try
        {
            var book = await _db.Books.FindAsync(BookId);
            if (book is null)
                return false;

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Tuple<List<BookDTO>, int> GetAllBooks(DateTime fromdate, DateTime toDate, int pageId = 1, string bookTitle = "", string authorName = "", int categoryId = 0, double startPrice = 0, double endPrice = 0, int publisherId = 0)
    {
        try
        {
            IQueryable<Book> books = _db.Books;

            if (!string.IsNullOrEmpty(bookTitle))
            {
                books = books.Where(b => b.Title.Contains(bookTitle));
            }

            if (!string.IsNullOrEmpty(authorName))
            {
                var authorId = _authorRepository.GetAuthorIdByName(authorName);

                books = books.Where(b => b.AuthorId == authorId);
            }

            if (categoryId != 0)
            {
                var requestedBookIds = _db.BookCategories.Where(b => b.CatId == categoryId).Select(b => b.BookId);
                books = books.Where(b => requestedBookIds.Contains(b.BookId));
            }

            if (DateTime.MinValue != fromdate)
            {
                //DateTime FromDate = DateTime.ParseExact(fromdate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                books = books.Where(u => u.CreatedDate >= fromdate);
            }

            if (DateTime.MinValue != toDate)
            {
                //DateTime ToDate = DateTime.ParseExact(toDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                books = books.Where(u => u.CreatedDate <= toDate);
            }

            if (startPrice > 0)
            {
                books = books.Where(b => b.Price >= startPrice);
            }

            if (endPrice > 0)
            {
                books = books.Where(b => b.Price <= endPrice);
            }

            if (publisherId != 0)
            {
                books = books.Where(b => b.PublisherId == publisherId);
            }


            var bookDTO = _mapper.Map<IQueryable<Book>, List<BookDTO>>(books);

            int take = 12;
            int skip = (pageId - 1) * take;
            var pageCount = bookDTO.Count() / take;
            if (pageCount % 2 != 0)
            {
                pageCount++;
            }

            var query = bookDTO.OrderByDescending(c => c.CreatedDate).Skip(skip).Take(take).ToList();

            return Tuple.Create(query, pageCount);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<BookDTO> GetBookById(int BookId)
    {
        try
        {
            var bookDTO = await _db.Books.FindAsync(BookId);
            if (bookDTO is null)
                return null;

            var book = _mapper.Map<Book, BookDTO>(bookDTO);
            return book;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<BookDTO>> GetBooksByPublisherId(int PublisherId)
    {
        try
        {
            var books = await _db.Books.Where(b => b.PublisherId == PublisherId).ToListAsync();
            if (books is null)
                return null;

            var booksDTO = _mapper.Map<List<Book>, List<BookDTO>>(books);
            return booksDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<BookDTO>> GetBooksByAuthorId(int AuthorId)
    {
        try
        {
            var books = await _db.Books
                .Where(b => b.AuthorId == AuthorId)
                .Take(8)
                .OrderByDescending(b=>b.CreatedDate)
                .ToListAsync();

            if (books is null)
                return null;

            var booksDTO = _mapper.Map<List<Book>, List<BookDTO>>(books);
            return booksDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<BookDTO> UpdateBook(int BookId, BookDTO BookDTO)
    {
        try
        {
            if (BookId != BookDTO.BookId)
                return null;

            var book = await _db.Books.FindAsync(BookId);
            var convertedBook = _mapper.Map<BookDTO, Book>(BookDTO, book);
            var UpdatedBook = _db.Books.Update(convertedBook);
            var UpdatedBookDTO = _mapper.Map<Book, BookDTO>(convertedBook);
            return UpdatedBookDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<int>> GetBookCategoryIds(int BookId)
    {
        try
        {
            var BookCategoryIds = await _db.BookCategories.Where(b => b.BookId == BookId).Select(b => b.CatId).ToListAsync();
            return BookCategoryIds;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<List<BookDTO>> GetBooksByCategoryId(int categoryId)
    {
        try
        {
            var BookIds = await _db.BookCategories
                .Where(b => b.CatId == categoryId)
                .Select(b => b.BookId)
                .ToListAsync();

            var books = await _db.Books
                .Where(b => BookIds.Contains(b.BookId))
                .OrderByDescending(b => b.CreatedDate).ToListAsync();

            if (books is null)
                return null;

            var booksDTO = _mapper.Map<List<Book>, List<BookDTO>>(books);

            return booksDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<BookDTO>> GetAllBooksWithoutPagination()
    {
        try
        {
            var books = await _db.Books.ToListAsync();
            if (books is null)
                return null;

            var booksDTO = _mapper.Map<List<Book>, List<BookDTO>>(books);

            return booksDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<string>> GetbookCategoryTitle(int BookId)
    {
        try
        {
            var BookCategoryIds = await _db.BookCategories.Where(b => b.BookId == BookId).Select(b => b.CatId).ToListAsync();

            var categoryTitles = await _db.Categories.Where(c => BookCategoryIds.Contains(c.catId)).Select(c => c.CatTitle).ToListAsync();
            if (categoryTitles is null)
                return null;
            return categoryTitles;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string GetBookTitle(int bookId)
    {
        try
        {
            var bookTitle = _db.Books.FirstOrDefault(b => b.BookId == bookId).Title;
            if (bookTitle is null)
                return null;

            return bookTitle;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<BookDTO>> GetLatestBooks()
    {
        try
        {
            var books = await _db.Books.OrderByDescending(b => b.CreatedDate).Take(8).ToListAsync();
            var booksDTO = _mapper.Map<List<Book>, List<BookDTO>>(books);

            return booksDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> DecreaseQuantity(int BookId)
    {
        try
        {
            var book = await _db.Books.FindAsync(BookId);

            if (book is null)
                return false;

            book.Quantity  -= 1;

            _db.Books.Update(book);
            await _db.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<List<BookDTO>> GetBookBySearchTxt(string q)
    {
        try
        {
            var books = await _db.Books
                .Where(b => b.Tags.Contains(q) || b.Title.Contains(q))
                .OrderByDescending(b => b.CreatedDate)
                .ToListAsync();

            if (books is null)
                return null;


            var booksDTO = _mapper.Map<List<Book>,List<BookDTO>>(books);

            return booksDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<string> GetbookAuthorName(int BookId)
    {
        try
        {
            var book = await _db.Books.FindAsync(BookId);

            if (book is null)
                return null;

            var author = await _db.Authors.FindAsync(book.AuthorId);

            if (author is null)
                return null;

            return author.AuthorName;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
