
namespace Bookshop_Business.Repository.IRepository;

public interface IAuthorRepository
{
    public Task<Tuple<List<AuthorDTO>, int>> GetAllAuthors(int pageId = 1,string authorName="");
    public Task<AuthorDTO> GetAuthorById(int AuthorId);
    public Task<string> GetAuthorNameById(int AuthorId);
    public int GetAuthorIdByName(string AuthorName);
    public Task<AuthorDTO> GetAuthorByBookId(int bookId);
    public Task<bool> AddAuthor(AuthorDTO authorDTO);
    public Task<bool> UpdateAuthor(int AuthorId, AuthorDTO authorDTO);
    public Task<bool> DeleteAuthorById(int AuthorId);
    public Task<bool> DoesAuthorExists(string authorName);
}
