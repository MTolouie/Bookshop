
namespace Bookshop_Business.Repository.IRepository;

public interface IAddressRepository
{
    public Task<List<AddressDTO>> GetAllUserAddresses(string UserId);
    public Task<AddressDTO> GetAddressById(int addressId);
    public Task<AddressDTO> GetSelectedAddressByUserId(string userId);
    public Task<bool> AddAddress(AddressDTO address);
    public Task<bool> EditAddress(AddressDTO address);
    public Task<bool> DeleteAddress(int addressId);
    public Task<bool> DoesAddressExists(AddressDTO addressDTO);
    public Task<bool> ChangeAddressStatus(int addressId, string userId);


}
