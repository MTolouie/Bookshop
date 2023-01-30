using Bookshop_BlazorClient.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Bookshop_BlazorClient.Services;

public class Repository<T> : IRepository<T> where T : class
{


    private readonly IHttpClientFactory _clientFactory;

    public Repository(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;

    }
    public async Task<bool> CreateAsync(string url, T objToCreate,string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        if (objToCreate != null)
        {
            request.Content = new StringContent(
                JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
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

    public async Task<bool> DeleteAsync(string url, int Id, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, url + Id);

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

    public async Task<IEnumerable<T>> GetAllAsync(string url)
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
            return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
        }

        return null;

    }

    public async Task<T> GetAsync(string url, int Id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url + Id);

        var client = _clientFactory.CreateClient();

        //if (token != null && token.Length != 0)
        //{
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}

        HttpResponseMessage response = await client.SendAsync(request);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        return null;
    }

    public async Task<bool> UpdateAsync(string url,string token, T objToUpdate)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, url);
        if (objToUpdate != null)
        {
            request.Content = new StringContent(
                JsonConvert.SerializeObject(objToUpdate), Encoding.UTF8, "application/json");
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
}
