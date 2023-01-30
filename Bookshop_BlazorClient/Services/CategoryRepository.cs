using Newtonsoft.Json;

namespace Bookshop_BlazorClient.Services;

public class CategoryRepository : Repository<CategoryDTO>,ICategoryRepository
{
    private readonly IHttpClientFactory _clientFactory;

    public CategoryRepository(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;

    }

    public async Task<string> GetCategoryTitleById(string url, string action, int categoryId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url + action + "/" + categoryId);

        var client = _clientFactory.CreateClient();

        //if (token != null && token.Length != 0)
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return jsonString;/*JsonConvert.DeserializeObject<string>(jsonString);*/
        }

        return null;
    }
}
