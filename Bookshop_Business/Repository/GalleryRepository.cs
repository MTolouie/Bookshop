
namespace Bookshop_Business.Repository;

public class GalleryRepository : IGalleryRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GalleryRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public async Task<bool> AddGallery(GalleryDTO galleryDTO)
    {
        try
        {
            var gallery = _mapper.Map<GalleryDTO,Gallery>(galleryDTO);
            _db.Gallery.Add(gallery);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteGalleryById(int Id)
    {
        try
        {
            var gallery = await _db.Gallery.FindAsync(Id);
            if (gallery is null)
                return false;

            _db.Gallery.Remove(gallery);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<List<GalleryDTO>> GetAllGalleriesByBookId(int bookId)
    {
        try
        {
            var galleries = await  _db.Gallery.Where(a => a.BookId == bookId).ToListAsync();
            var galleriesDTO = _mapper.Map<List<Gallery>,List<GalleryDTO>>(galleries);
            return galleriesDTO;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<GalleryDTO> GetGalleryById(int Id)
    {
        try
        {
            var gallery = await _db.Gallery.FindAsync(Id);
            if (gallery is null)
                return null;

            var galleryDTO = _mapper.Map<Gallery,GalleryDTO>(gallery);
            return galleryDTO;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
