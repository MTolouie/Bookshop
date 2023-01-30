using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Bookshop_BlazorClient.Services;

public class CommentRepository : Repository<CommentDTO>,ICommentRepository
{
    private readonly IHttpClientFactory _clientFactory;

    public CommentRepository(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;

    }

    public async Task<List<CommentDTO>> GetCommentsByBookId(string url,string action,int bookId
        )
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url + bookId);

        var client = _clientFactory.CreateClient();

        //if (token != null && token.Length != 0)
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CommentDTO>>(jsonString);
        }

        return null;
    }
}
