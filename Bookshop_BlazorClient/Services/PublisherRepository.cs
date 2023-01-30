using Newtonsoft.Json;

namespace Bookshop_BlazorClient.Services;

public class PublisherRepository : Repository<PublisherDTO>, IPublisherRepository
{
    private readonly IHttpClientFactory _clientFactory;

    public PublisherRepository(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<List<PublisherDTO>> GetPublishersWithoutPagination(string url,string action)
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
            return JsonConvert.DeserializeObject<List<PublisherDTO>>(jsonString);
        }

        return null;
    }
}
