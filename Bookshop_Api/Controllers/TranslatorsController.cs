using Microsoft.AspNetCore.Mvc;

namespace Bookshop_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TranslatorsController : ControllerBase
{
    private readonly ITranslatorRepository _translatorRepository;
    public TranslatorsController(ITranslatorRepository translatorRepository)
    {
        _translatorRepository = translatorRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TranslatorDTO>))]
    public async Task<IActionResult> GetTranslators()
    {
        var translators = await _translatorRepository.GetAllTranslators();
        if (translators is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(translators);
    }

    [HttpGet("{Id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TranslatorDTO))]
    public async Task<IActionResult> GetTranslatorById(int Id)
    {
        var translator = await _translatorRepository.GetTranslatorById(Id);
        if (translator is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(translator);
    }
}
