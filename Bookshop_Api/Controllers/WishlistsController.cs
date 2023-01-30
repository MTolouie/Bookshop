using BookShop_Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = SD.Role_Customer)]
    public class WishlistsController : Controller
    {
        private readonly IWishlistRepository _wishlistRepository;

        public WishlistsController(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        
        [HttpGet("{userId}/{pageId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WishlistDTO>))]

        public async Task<IActionResult> GetAllUserWishlist(string userId, int pageId = 1)
        {
            var (wishlists,pageCount) = await _wishlistRepository.GetAllUserWishlist(userId, pageId);
            if (wishlists is null)
                return StatusCode(StatusCodes.Status204NoContent);

            var tuple = Tuple.Create(wishlists, pageCount);

            return Ok(tuple);
        }
        
        [HttpGet("[action]/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]

        public async Task<IActionResult> GetNumberOfUserWishlist(string userId)
        {
            var wishlists = await _wishlistRepository.GetNumberOfUserWishlist(userId);
            
            return Ok(wishlists);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<bool>))]

        public async Task<IActionResult> AddWishlist([FromBody] WishlistDTO wishlistDTO)
        {
            if (wishlistDTO is null)
                return BadRequest();

            var DoesExist = await _wishlistRepository.DoesBookExistsInWishlist(wishlistDTO.UserId,wishlistDTO.BookId);
            if (DoesExist == true)
            {
                return BadRequest();
            }

            var IsCreated = await _wishlistRepository.AddWishlist(wishlistDTO);

            if (IsCreated)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed);
            }
        }

        [HttpDelete("{wishlistId:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]

        public async Task<IActionResult> DeleteWishlist(int wishlistId)
        {
 
            var IsDeleted = await _wishlistRepository.DeleteWishlistById(wishlistId);

            if (IsDeleted)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed);
            }
        }
    }
}
