
namespace Bookshop_Business.Repository.IRepository;

public interface IBookCoverFormatRepository
{
    public Task<List<BookCoverFormatDTO>> GetAllBookCoverFormats(string coverFormatTitle = "");
    public Task<BookCoverFormatDTO> GetBookCoverFormatById(int coverFormatId);
    public Task<bool> AddBookCoverFormat(BookCoverFormatDTO coverFormatDTO);
    public Task<bool> IsBookCoverFormatTitleUnique(string coverFormatTitle);
    public Task<bool> DeleteBookCoverFormat(int coverFormatId);

    public Task<bool> UpdateBookCoverFormat(int coverFormatId, BookCoverFormatDTO coverFormatDTO);
}
