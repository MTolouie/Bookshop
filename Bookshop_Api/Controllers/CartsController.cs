using BookShop_Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace Bookshop_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = SD.Role_Customer)]
public class CartsController : ControllerBase
{
    private readonly ICartRepository _cartRepository;

    public CartsController(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    [HttpPost("[action]/{bookId}/{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddToCart(int bookId,string userId)
    {
        var IsAdded = await _cartRepository.AddToCart(bookId,userId);

        if (IsAdded)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }


    [HttpGet("[action]/{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserCart(string userId)
    {
        var userCart = await _cartRepository.GetUserCart(userId);

        return Ok(userCart);
    }


    [HttpDelete("{cartId:int}/{cartDetailId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteFromCart(int cartId,int cartDetailId)
    {
        var IsDeleted = await _cartRepository.DeleteFromCart(cartId,cartDetailId);

        if (IsDeleted)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
    
    [HttpGet("{cartId:int}/{stripeSessionId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> MarkCartIsFinally(int cartId,string stripeSessionId)
    {

        var session = new SessionService();
        var sessionDetails = session.Get(stripeSessionId);

        if(sessionDetails.PaymentStatus == "paid")
        {
            var IsUpdated = await _cartRepository.MarkCartIsFinally(cartId);

            if (IsUpdated)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        else
        {
            return BadRequest();
        }

        
    }


    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNumberOfUserCart(string userId)
    {
        var numberOfCarts = await _cartRepository.GetNumberOfUserCart(userId);

        return Ok(numberOfCarts);
    }
    
    [HttpGet("[action]/{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserRecentOrders(string userId)
    {
        var carts = await _cartRepository.GetUserRecentOrders(userId);

        if(carts is not null)
        { 
        
        return Ok(carts);
        }
        else
        {
            return BadRequest();
        }
    }
}
