using Microsoft.AspNetCore.Mvc;

namespace Bookshop_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublishersController : ControllerBase
{
    private readonly IPublisherRepository _publisherRepository;
    public PublishersController(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PublisherDTO>))]
    public async Task<IActionResult> GetAllPublishers()
    {
        var publishers = await _publisherRepository.GetAllPublishers();
        if (publishers is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(publishers);
    }

    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PublisherDTO>))]
    public async Task<IActionResult> GetPublishersWithoutPagination()
    {
        var publishers = await _publisherRepository.GetPublishersWithoutPagination();
        if (publishers is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(publishers);
    }

    [HttpGet("{Id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PublisherDTO))]
    public async Task<IActionResult> GetPublisherId(int Id)
    {
        var publisher =  _publisherRepository.GetPublisherById(Id);
        if (publisher is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(publisher);
    }
}
