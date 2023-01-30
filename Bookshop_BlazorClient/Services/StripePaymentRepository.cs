using System.Text;

namespace Bookshop_BlazorClient.Services;

public class StripePaymentRepository : IStripePaymentRepository
{
    private readonly HttpClient _client;
    public StripePaymentRepository(HttpClient client)
    {
        _client = client;
    }

    public async Task<SuccessPaymentDTO> Checkout(string url, string action,StripePaymentDTO paymentDTO,string token)
    {

        var content = JsonConvert.SerializeObject(paymentDTO);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"{url}{action}", bodyContent);
        var res =  response.Content.ReadAsStringAsync().Result;
        //if (token != null && token.Length != 0)
        //{
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}
        if (response.IsSuccessStatusCode)
        {
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SuccessPaymentDTO>(contentTemp);
            return result;
        }
        else
        {
            var contentTemp = await response.Content.ReadAsStringAsync();
            var errorModel = JsonConvert.DeserializeObject<StripeErrorDTO>(contentTemp);
            throw new Exception(errorModel.ErrorMessage);
        }
    }

    
}
