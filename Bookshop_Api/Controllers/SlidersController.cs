using Microsoft.AspNetCore.Mvc;

namespace Bookshop_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SlidersController : ControllerBase
{
    private readonly ISliderRepository _sliderRepository;

    public SlidersController(ISliderRepository sliderRepository)
    {
        _sliderRepository = sliderRepository;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SlidersDTO>))]
    public async Task<IActionResult> GetActiveSliders()
    {
        var sliders = await _sliderRepository.GetActiveSliders();

        if (sliders is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(sliders);
    }
}
