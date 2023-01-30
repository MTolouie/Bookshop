using Bookshop_Models.ViewModels;
using Newtonsoft.Json;

namespace Bookshop_BlazorClient.Services;

public class BookRepository : Repository<BookDTO>,IBookRepository
{
    private readonly IHttpClientFactory _clientFactory;

    public BookRepository(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        _clientFactory = clientFactory;

    }

    public async Task<bool> DecreaseQuantity(string url, string action,int bookId, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url + action + "/" + bookId);

        var client = _clientFactory.CreateClient();

        if (token != null && token.Length != 0)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            //var jsonString = await response.Content.ReadAsStringAsync();
            //return JsonConvert.DeserializeObject<List<string>>(jsonString);
            return true;
        }

        return false;
    }

    public async Task<string> GetbookAuthorName(string url, string action, int bookId)
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
            //return JsonConvert.DeserializeObject<string>(jsonString);
            //return true;
            return jsonString;
        }

        return null;
    }

    public async Task<IEnumerable<BookDTO>> GetBookBySearchTxt(string url, string action, string q)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url + action + "/" + q);

        var client = _clientFactory.CreateClient();

        //if (token != null && token.Length != 0)
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<BookDTO>>(jsonString);
            //return true;
        }

        return null;
    }

    //public async Task<IEnumerable<WishlistBookViewModel>> GetBooksForWishlist(string url, string userId,string? token)
    //{
    //    var request = new HttpRequestMessage(HttpMethod.Get, url+userId);

    //    var client = _clientFactory.CreateClient();

    //    if (token != null && token.Length != 0)
    //    {
    //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    //    }

    //    HttpResponseMessage response = await client.SendAsync(request);
    //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
    //    {
    //        var jsonString = await response.Content.ReadAsStringAsync();
    //        return JsonConvert.DeserializeObject<IEnumerable<WishlistBookViewModel>>(jsonString);
    //    }

    //    return null;
    //}

    public async Task<List<string>> GetbookCategoryTitle(string url, string action, int bookId)
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
            return JsonConvert.DeserializeObject<List<string>>(jsonString);
        }

        return null;
    }

    public async Task<IEnumerable<BookDTO>> GetBooksByAuthorId(string url, string action, int authorId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url + action + "/" + authorId);

        var client = _clientFactory.CreateClient();

        //if (token != null && token.Length != 0)
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<BookDTO>>(jsonString);
        }

        return null;
    }

    public async Task<List<BookDTO>> GetBooksByCategoryId(string url,string action,int categoryId)
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
            return JsonConvert.DeserializeObject<List<BookDTO>>(jsonString);
        }

        return null;
    }

    public async Task<Tuple<List<BookDTO>,int>> GetBooksWithPagination(string url,string action,int pageId=0,int categoryId=0, double startPrice = 0, double endPrice = 0, int publisherId = 0)
    {
        var customUrl = $"{url}{action}/{pageId}/{categoryId}/{endPrice}/{startPrice}/{publisherId}";
        var request = new HttpRequestMessage(HttpMethod.Get, customUrl);

        var client = _clientFactory.CreateClient();

        //if (token != null && token.Length != 0)
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Tuple<List<BookDTO>,int>>(jsonString);
        }

        return null;
    }

    public async Task<List<BookDTO>> GetLatestBooks(string url,string action)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        var client = _clientFactory.CreateClient();

        //if (token != null && token.Length != 0)
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<BookDTO>>(jsonString);
        }

        return null;
    }
}
