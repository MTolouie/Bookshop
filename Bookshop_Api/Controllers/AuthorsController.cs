using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorsController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AuthorDTO>))]

    public async Task<IActionResult> GetAllAuthors()
    {
        //var (authors,pageCount) = await _authorRepository.GetAllAuthors();
        var authors = await _authorRepository.GetAllAuthors();
        if (authors is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(authors);
    }


    [HttpGet("{Id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthorDTO))]
    public async Task<IActionResult> GetAuthorById(int Id)
    {
        var author = await _authorRepository.GetAuthorById(Id);
        if (author is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(author);
    }
}
