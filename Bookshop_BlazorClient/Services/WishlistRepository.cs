


namespace Bookshop_BlazorClient.Services;

public class WishlistRepository : Repository<WishlistDTO>, IWishlistRepository
{
    private IHttpClientFactory _clientFactory;
    public WishlistRepository(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<Tuple<IEnumerable<WishlistDTO>, int>> GetAllUserWishlists(string url, string userId, string? token, int pageId = 1)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{url}{userId}/{pageId}");

        var client = _clientFactory.CreateClient();

        if (token != null && token.Length != 0)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Tuple<IEnumerable<WishlistDTO>, int>>(jsonString);
        }

        return null;
    }

    public async Task<int> GetNumberOfUserWishlist(string url, string action, string userId, string? token)
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
            return JsonConvert.DeserializeObject<int>(jsonString);
        }
        else
        {
            return 0;
        }

    }
}