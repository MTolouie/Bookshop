
namespace Bookshop_Business.Repository.IRepository;

public interface IGalleryRepository
{
    public Task<List<GalleryDTO>> GetAllGalleriesByBookId(int bookId);
    public Task<GalleryDTO> GetGalleryById(int Id);
    public Task<bool> DeleteGalleryById(int Id);
    public Task<bool> AddGallery(GalleryDTO galleryDTO);
}
