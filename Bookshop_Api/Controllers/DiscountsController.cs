using Microsoft.AspNetCore.Mvc;

namespace Bookshop_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountsController : Controller
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountsController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DiscountDTO>))]
        public async Task<IActionResult> GetAllDiscounts()
        {
            var discounts = await _discountRepository.GetAllDiscounts();
            if (discounts is null)
                return StatusCode(StatusCodes.Status204NoContent);

            return Ok(discounts);
        }


        [HttpGet("[action]/{bookId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> GetBookDiscount(int bookId)
        {
            var discount = await _discountRepository.GetDiscountByBookId(bookId);
            if (discount is null)
                return StatusCode(StatusCodes.Status204NoContent);

            return Ok(discount);
        }


        
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DiscountDTO>))]
        public async Task<IActionResult> GetTwoLatestDiscounts()
        {
            var discounts = await _discountRepository.GetTwoLatestDiscounts();
            if (discounts is null)
                return StatusCode(StatusCodes.Status204NoContent);

            return Ok(discounts);
        }
    }
}
