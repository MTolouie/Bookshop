using Microsoft.AspNetCore.Mvc;

namespace Bookshop_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookCoverFormatsController : ControllerBase
{
    private readonly IBookCoverFormatRepository _bookCoverFormatRepository;

    public BookCoverFormatsController(IBookCoverFormatRepository bookCoverFormatRepository)
    {
        _bookCoverFormatRepository = bookCoverFormatRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookCoverFormatDTO>))]
    public async Task<IActionResult> GetAllCoverFormats()
    {
        var coverFormats = await _bookCoverFormatRepository.GetAllBookCoverFormats();
        if (coverFormats is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(coverFormats);
    }


    [HttpGet("{Id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookCoverFormatDTO))]
    public async Task<IActionResult> GetCoverFormatById(int Id)
    {
        var coverFormat = await _bookCoverFormatRepository.GetBookCoverFormatById(Id);
        if (coverFormat is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(coverFormat);
    }



}
