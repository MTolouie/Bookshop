
namespace Bookshop_Business.Repository;

public class BookCoverFormatRepository : IBookCoverFormatRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public BookCoverFormatRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<bool> AddBookCoverFormat(BookCoverFormatDTO coverFormatDTO)
    {
        try
        {

            var coverFormat = _mapper.Map<BookCoverFormatDTO, BookCoverFormat>(coverFormatDTO);
            coverFormat.CoverFormatTitle.Trim();
            _db.BookCoverFormats.Add(coverFormat);
            await _db.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteBookCoverFormat(int coverFormatId)
    {

        try
        {
            var coverFormat = await _db.BookCoverFormats.FindAsync(coverFormatId);

            if (coverFormat is null)
                return false;

            _db.BookCoverFormats.Remove(coverFormat);
            await _db.SaveChangesAsync();
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<List<BookCoverFormatDTO>> GetAllBookCoverFormats(string coverFormatTitle = "")
    {
        IQueryable<BookCoverFormat> coverFormats = _db.BookCoverFormats;

        if (!string.IsNullOrEmpty(coverFormatTitle))
        {
            coverFormats = coverFormats.Where(c => c.CoverFormatTitle.Contains(coverFormatTitle.ToLower().Trim()));
        }


        var coverFormatsDTO = _mapper.Map<IQueryable<BookCoverFormat>, List<BookCoverFormatDTO>>(coverFormats);


        //int take = 5;
        //int skip = (pageId - 1) * take;
        //var pageCount = categoriesDTO.Count() / take;
        //if (pageCount % 2 != 0)
        //{
        //    pageCount++;
        //}
        var query = coverFormatsDTO.OrderByDescending(c => c.CoverFormatId).ToList();
        return query;
    }



    public async Task<BookCoverFormatDTO> GetBookCoverFormatById(int coverFormatId)
    {
        try
        {
            var coverFormat = await _db.BookCoverFormats.FindAsync(coverFormatId);

            if (coverFormat is null)
                return null;

            var coverFormatDTO = _mapper.Map<BookCoverFormat, BookCoverFormatDTO>(coverFormat);
            return coverFormatDTO;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> IsBookCoverFormatTitleUnique(string coverFormatTitle)
    {
        var title = await _db.BookCoverFormats.FirstOrDefaultAsync(c => c.CoverFormatTitle.ToLower() == coverFormatTitle.ToLower());
        if (title is not null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public async Task<bool> UpdateBookCoverFormat(int coverFormatId, BookCoverFormatDTO coverFormatDTO)
    {
        try
        {
            if (coverFormatDTO.CoverFormatId != coverFormatId)
                return false;

            var coverFormatFromDb = await _db.BookCoverFormats.FindAsync(coverFormatId);
            coverFormatFromDb.CoverFormatTitle = coverFormatDTO.CoverFormatTitle;

            _db.BookCoverFormats.Update(coverFormatFromDb);
            await _db.SaveChangesAsync();

            return true;


        }
        catch (Exception ex)
        {
            return false;
        }
    }

  
}
