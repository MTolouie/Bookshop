
namespace Bookshop_Business.Repository;

public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public AddressRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<bool> AddAddress(AddressDTO address)
    {
        try
        {
            var Address = _mapper.Map<AddressDTO, Address>(address);
            _db.Addresses.Add(Address);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> ChangeAddressStatus(int addressId, string userId)
    {
        try
        {
            //var address = await _db.addresses.findasync(addressid);

            //if (address is null)
            //return false;

            var userAddresses = await _db.Addresses.Where(a => a.UserId == userId).ToListAsync();

            if(userAddresses is null || userAddresses.Count < 1)
                return false;


            foreach (var address in userAddresses)
            {
                if(address.AddressId == addressId)
                {
                    address.IsSelected = true;
                }
                else
                {
                    address.IsSelected = false;
                }

                _db.Addresses.Update(address);
                await _db.SaveChangesAsync();
            }

            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAddress(int addressId)
    {
        try
        {
            var address = await _db.Addresses.FindAsync(addressId);
            if (address == null)
                return false;

            _db.Addresses.Remove(address);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DoesAddressExists(AddressDTO addressDTO)
    {
        try
        {
            var addressFromDb = await _db.Addresses
                .Where(c => c.AddressTitle.ToLower() == addressDTO.AddressTitle.ToLower() && c.AddressId != addressDTO.AddressId)
                .SingleOrDefaultAsync();

            if (addressFromDb is not null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> EditAddress(AddressDTO addressDTO)
    {
        try
        {

            var addressFromDb = await _db.Addresses.FindAsync(addressDTO.AddressId);
            if (addressFromDb is null)
                return false;

            var UpdatedAddress = _mapper.Map<AddressDTO, Address>(addressDTO, addressFromDb);

            _db.Addresses.Update(UpdatedAddress);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<AddressDTO> GetAddressById(int addressId)
    {
        try
        {
            var address = await _db.Addresses.FindAsync(addressId);
            if (address is null)
                return null;

            var addressDTO = _mapper.Map<Address, AddressDTO>(address);

            return addressDTO;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<AddressDTO>> GetAllUserAddresses(string UserId)
    {
        try
        {
            var addresses = await _db.Addresses.Where(a => a.UserId == UserId).ToListAsync();
            if (addresses is null)
                return null;

            var addressesDTO = _mapper.Map<List<Address>, List<AddressDTO>>(addresses);

            return addressesDTO;

        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<AddressDTO> GetSelectedAddressByUserId(string userId)
    {
        try
        {
            var address = await _db.Addresses
                .Where(a => a.UserId == userId && a.IsSelected == true)
                .SingleOrDefaultAsync();

            if (address is null)
                return null;

            var addressDTO = _mapper.Map<Address,AddressDTO>(address);

            return addressDTO;

        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
