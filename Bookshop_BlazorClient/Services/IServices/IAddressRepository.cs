namespace Bookshop_BlazorClient.Services.IServices;

public interface IAddressRepository : IRepository<AddressDTO>
{
    public Task<bool> DoesAddressExists(string url,string action, string token,AddressDTO addressDTO);
    public Task<IEnumerable<AddressDTO>> GetUserAddresses(string url,string userId, string token);
    public Task<AddressDTO> GetSelectedAddressByUserId(string url, string action, string userId, string token);
    public Task<AddressDTO> GetAddressById(string url,string action ,int addressId, string token);
    public Task<bool> ChangeAddressStatus(string url,string action ,int addressId,string userId, string token);
}
