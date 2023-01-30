using Microsoft.AspNetCore.Mvc;

namespace Bookshop_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{

    private readonly ICategoryRepository _categoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }



    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(List<CategoryDTO>))]

    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryRepository.GetAllCategoriesForCreateOrEdit();
        if (categories is null)
            return StatusCode(StatusCodes.Status204NoContent);

        return Ok(categories);
    }


    [HttpGet("{Id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK ,Type = typeof(CategoryDTO))]
    public async Task<IActionResult> GetCategoryById(int Id)
    {
        var category = await _categoryRepository.GetCategoryById(Id);
        if (category is null)
            return StatusCode(StatusCodes.Status404NotFound);

        return Ok(category);
    }

    [HttpGet("[action]/{categoryId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    public async Task<IActionResult> GetCategoryTitleById(int categoryId)
    {
        var categoryTitle = await _categoryRepository.GetCategoryTitleById(categoryId);
        if (categoryTitle is null)
            return StatusCode(StatusCodes.Status404NotFound);

        return Ok(categoryTitle);
    }
}
