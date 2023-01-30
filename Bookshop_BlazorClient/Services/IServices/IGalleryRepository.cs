namespace Bookshop_BlazorClient.Services.IServices;

public interface IGalleryRepository : IRepository<GalleryDTO>
{
    Task<List<GalleryDTO>> GetAllGalleriesByBookId(string url,string action,int bookId);
}
