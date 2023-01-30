using Newtonsoft.Json;

namespace Bookshop_BlazorClient.Services;

public class GalleryRepository : Repository<GalleryDTO>, IGalleryRepository
{
    private readonly IHttpClientFactory _clientFactory;

    public GalleryRepository(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;

    }
    public async Task<List<GalleryDTO>> GetAllGalleriesByBookId(string url, string action,int bookId)
    {

        var request = new HttpRequestMessage(HttpMethod.Get, url+ action + "/" +bookId);

        var client = _clientFactory.CreateClient();

        //if (token != null && token.Length != 0)
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GalleryDTO>>(jsonString);
        }

        return null;
    }

}
