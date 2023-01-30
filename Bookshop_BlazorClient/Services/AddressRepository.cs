using System.Text;

namespace Bookshop_BlazorClient.Services;

public class AddressRepository : Repository<AddressDTO>,IAddressRepository
{
    private readonly IHttpClientFactory _clientFactory;
    public AddressRepository(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<bool> ChangeAddressStatus(string url, string action, int addressId, string userId, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{url}{action}/{addressId}/{userId}");

        var client = _clientFactory.CreateClient();

        if (token != null && token.Length != 0)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            //var jsonString = await response.Content.ReadAsStringAsync();
            //return JsonConvert.DeserializeObject<bool>(jsonString);
            return true;
        }

        return false;
    }

    public async Task<bool> DoesAddressExists(string url, string action,string token, AddressDTO addressDTO)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url + action);
        if (addressDTO != null)
        {
            request.Content = new StringContent(
                JsonConvert.SerializeObject(addressDTO), Encoding.UTF8, "application/json");
        }
        else
        {
            return false;
        }

        var client = _clientFactory.CreateClient();

        if (token != null && token.Length != 0)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<AddressDTO> GetAddressById(string url,string action, int addressId, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{url}{action}/{addressId}");

        var client = _clientFactory.CreateClient();

        if (token != null && token.Length != 0)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AddressDTO>(jsonString);
        }

        return null;
    }

    public async Task<AddressDTO> GetSelectedAddressByUserId(string url, string action, string userId, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{url}{action}/{userId}");

        var client = _clientFactory.CreateClient();

        if (token != null && token.Length != 0)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AddressDTO>(jsonString);
        }

        return null;
    }

    public async Task<IEnumerable<AddressDTO>> GetUserAddresses(string url, string userId, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url + userId);

        var client = _clientFactory.CreateClient();

        if (token != null && token.Length != 0)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<AddressDTO>>(jsonString);
        }

        return null;
    }
}
