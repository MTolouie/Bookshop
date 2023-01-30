namespace Bookshop_BlazorClient.Services;

public class CartRepository : Repository<CartDTO>, ICartRepository
{
    private readonly IHttpClientFactory _clientFactory;

    public CartRepository(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;

    }
    public async Task<bool> AddToCart(string url,string action, int bookId, string userId,string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{url}{action}/{bookId}/{userId}");
        //if (objToCreate != null)
        //{
        //    request.Content = new StringContent(
        //        JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
        //}
        //else
        //{
        //    return false;
        //}

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

    public async Task<bool> DeleteFromCart(string url, int cartId, int cartDetailId, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{url}{cartId}/{cartDetailId}");

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
        return false;
    }

    public async Task<int> GetNumberOfUserCart(string url, string userId, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{url}{userId}");

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
            //return true;
        }

        return 0;
    }

    public async Task<CartDTO> GetUserCart(string url, string action, string userId, string token)
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
            return JsonConvert.DeserializeObject<CartDTO>(jsonString);
            //return true;
        }

        return null;
    }

    public async Task<IEnumerable<CartDTO>> GetUserRecentOrders(string url, string action, string userId, string token)
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
            return JsonConvert.DeserializeObject<IEnumerable<CartDTO>>(jsonString);
            //return true;
        }

        return null;
    }

    public async Task<bool> MarkCartIsFinally(string url, int cartId,string stripeSessionId, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{url}{cartId}/{stripeSessionId}");

        var client = _clientFactory.CreateClient();

        if (token != null && token.Length != 0)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            //var jsonString = await response.Content.ReadAsStringAsync();
            //return JsonConvert.DeserializeObject<CartDTO>(jsonString);
            return true;
        }

        return false;
    }
}
