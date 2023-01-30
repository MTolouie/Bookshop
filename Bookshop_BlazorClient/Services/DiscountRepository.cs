using Newtonsoft.Json;

namespace Bookshop_BlazorClient.Services;

public class DiscountRepository : Repository<DiscountDTO>, IDiscountRepository
{
    private readonly IHttpClientFactory _clientFactory;

    public DiscountRepository(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;

    }
    public async Task<DiscountDTO> GetBookDiscount(string url, string action, int bookId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url + action + "/" + bookId);

        var client = _clientFactory.CreateClient();

        //if (token != null && token.Length != 0)
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DiscountDTO>(jsonString);
        }

        return null;
    }

    public async Task<List<DiscountDTO>> GetTwoLatestDiscounts(string url, string action)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url + action);

        var client = _clientFactory.CreateClient();

        //if (token != null && token.Length != 0)
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<DiscountDTO>>(jsonString);
        }

        return null;
    }
}
