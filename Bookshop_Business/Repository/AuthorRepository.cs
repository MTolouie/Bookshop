
namespace Bookshop_Business.Repository;

public class AuthorRepository : IAuthorRepository
{

    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public AuthorRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }



    public async Task<bool> AddAuthor(AuthorDTO authorDTO)
    {

        try
        {
            var author = _mapper.Map<AuthorDTO, Author>(authorDTO);

            _db.Authors.Add(author);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAuthorById(int AuthorId)
    {
        try
        {
            var author = await _db.Authors.FindAsync(AuthorId);
            if (author == null)
                return false;

            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DoesAuthorExists(string authorName)
    {
        try
        {
            var author = await _db.Authors.FirstOrDefaultAsync(a => a.AuthorName.ToLower() == authorName.ToLower().Trim());
            if (author is null)
                return true;
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public async Task<Tuple<List<AuthorDTO>, int>> GetAllAuthors(int pageId = 1, string authorName = "")
    {
        IQueryable<Author> authors = _db.Authors;

        if (!string.IsNullOrEmpty(authorName))
        {
            authors = authors.Where(a => a.AuthorName.Contains(authorName.ToLower().Trim()));
        }


        var authorsDTO = _mapper.Map<IQueryable<Author>, List<AuthorDTO>>(authors);


        int take = 5;
        int skip = (pageId - 1) * take;
        var pageCount = authorsDTO.Count() / take;
        if (pageCount % 2 != 0)
        {
            pageCount++;
        }
        var query = authorsDTO.OrderByDescending(a => a.AuthorID).Skip(skip).Take(take).ToList();
        return Tuple.Create(query, pageCount);

    }

    public async Task<AuthorDTO> GetAuthorByBookId(int bookId)
    {
        try
        {
            var Book = await _db.Books.FindAsync(bookId);
            if (Book is null)
                return null;

            var author = _db.Authors.Find(Book.AuthorId);
            if (author is null)
                return null;

            return _mapper.Map<Author, AuthorDTO>(author);

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<AuthorDTO> GetAuthorById(int AuthorId)
    {
        try
        {
            var author =  _db.Authors.Find(AuthorId);

            if (author == null)
                return null;

            var authorDTO = _mapper.Map<Author, AuthorDTO>(author);
            return authorDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public int GetAuthorIdByName(string AuthorName)
    {
        try
        {
            var authorId = _db.Authors.Where(a => a.AuthorName.ToLower().Trim() == AuthorName.ToLower().Trim()).Select(a=>a.AuthorID).SingleOrDefault();

            if (authorId <= 0)
                return 0;
 
            return authorId;

        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public async Task<string> GetAuthorNameById(int AuthorId)
    {
        try
        {
            var author = await _db.Authors.FindAsync(AuthorId);

            if (author is null)
                return null;

            return author.AuthorName;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> UpdateAuthor(int AuthorId, AuthorDTO authorDTO)
    {
        try
        {
            if (AuthorId != authorDTO.AuthorID)
                return false;


            var author = await _db.Authors.FindAsync(AuthorId);
            if (author is null)
                return false;

            var UpdatedAuthor = _mapper.Map<AuthorDTO, Author>(authorDTO, author);

            _db.Authors.Update(UpdatedAuthor);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
