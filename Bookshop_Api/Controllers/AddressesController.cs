using BookShop_Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = SD.Role_Customer)]
public class AddressesController : ControllerBase
{
    private readonly IAddressRepository _addressRepository;

    public AddressesController(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AddressDTO>))]
    public async Task<IActionResult> GetAllUserAddresses(string userId)
    {
        var addresses = await _addressRepository.GetAllUserAddresses(userId);

        if (addresses is null)
            return NoContent();

        return Ok(addresses);

    }

    [HttpGet("[action]/{addressId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDTO))]
    public async Task<IActionResult> GetAddressById(int addressId)
    {
        var address = await _addressRepository.GetAddressById(addressId);

        if (address is null)
            return NoContent();

        return Ok(address);

    }
    
    [HttpGet("[action]/{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressDTO))]
    public async Task<IActionResult> GetSelectedAddressByUserId(string userId)
    {
        var address = await _addressRepository.GetSelectedAddressByUserId(userId);

        if (address is null)
            return NoContent();

        return Ok(address);

    }

    [HttpGet("[action]/{addressId:int}/{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    public async Task<IActionResult> ChangeAddressStatus(int addressId, string userId)
    {
        var IsChanged = await _addressRepository.ChangeAddressStatus(addressId, userId);

        if (IsChanged)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }

    }


    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> EditAddress(AddressDTO addressDTO)
    {
        var IsEdited = await _addressRepository.EditAddress(addressDTO);

        if (IsEdited)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }


    [HttpDelete("{addressId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAddress(int addressId)
    {
        var IsDeleted = await _addressRepository.DeleteAddress(addressId);

        if (IsDeleted)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddAddress(AddressDTO addressDTO)
    {
        var IsAdded = await _addressRepository.AddAddress(addressDTO);

        if (IsAdded)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DoesAddressExists(AddressDTO addressDTO)
    {
        var DoesExists = await _addressRepository.DoesAddressExists(addressDTO);

        if (DoesExists)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }

    }
}
