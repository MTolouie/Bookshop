using Microsoft.AspNetCore.Mvc;

namespace Bookshop_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GalleriesController : Controller
{
    private readonly IGalleryRepository _galleryRepository;

    public GalleriesController(IGalleryRepository galleryRepository)
    {
        _galleryRepository = galleryRepository;
    }


    [HttpGet("[action]/{bookId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GalleryDTO>))]
    public async Task<IActionResult> GetAllGalleriesByBookId(int bookId)
    {
        var galleries = await _galleryRepository.GetAllGalleriesByBookId(bookId);
        if (galleries is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(galleries);
    }
}
