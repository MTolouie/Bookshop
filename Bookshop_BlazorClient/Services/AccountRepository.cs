using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Bookshop_BlazorClient.Services;

public class AccountRepository : IAccountRepository
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly IHttpClientFactory _clientFactory;

    public AccountRepository(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider, IHttpClientFactory clientFactory)
    {
        _client = client;
        _authStateProvider = authStateProvider;
        //((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        _localStorage = localStorage;
        _clientFactory = clientFactory;
    }

    public async Task LogOut()
    {
        await _localStorage.RemoveItemAsync(SD.Local_Token);
        await _localStorage.RemoveItemAsync(SD.Local_UserDetails);
        ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        _client.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<AuthenticationResponseDTO> SignIn(AuthenticationDTO authentication)
    {
        var content = JsonConvert.SerializeObject(authentication);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"{SD.AccountApiPath}SignIn", bodyContent);
        var contentTemp = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(contentTemp);

        if (response.IsSuccessStatusCode)
        {
            await _localStorage.SetItemAsync(SD.Local_Token, result.Token);
            await _localStorage.SetItemAsync(SD.Local_UserDetails, result.userDTO);
            ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(result.Token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return new AuthenticationResponseDTO { IsAuthSuccessful = true };
        }
        else
        {

            return new AuthenticationResponseDTO { IsAuthSuccessful = false, ErrorMessage = result.ErrorMessage };
        }
    }

    public async Task<RegisterationResponseDTO> SignUp(UserRequestDTO userRequest)
    {
        var content = JsonConvert.SerializeObject(userRequest);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"{SD.AccountApiPath}signup", bodyContent);
        var contentTemp = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<RegisterationResponseDTO>(contentTemp);

        if (response.IsSuccessStatusCode)
        {

            return new RegisterationResponseDTO { IsRegisterationSuccessful = true, Message = result.Message };
        }
        else
        {
            return result;
        }
    }

    public async Task<bool> ForgotPassword(ForgotPasswordDTO forgotPassword)
    {
        var content = JsonConvert.SerializeObject(forgotPassword);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"{SD.AccountApiPath}ForgotPassword", bodyContent);
        var contentTemp = await response.Content.ReadAsStringAsync();
        //var result = JsonConvert.DeserializeObject<bool>(contentTemp);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //public async Task<bool> ResetPasswordCodeConfrim(string code)
    //{
    //    var request = new HttpRequestMessage(HttpMethod.Get, $"{SD.AccountApiPath}ResetPasswordCodeConfrim?code={code}");

    //    var client = _clientFactory.CreateClient();

    //    //if (token != null && token.Length != 0)
    //    //{
    //    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    //    //}

    //    HttpResponseMessage response = await client.SendAsync(request);
    //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    public async Task<bool> ResetPassword(ResetPasswordDTO resetPassword)
    {
        var content = JsonConvert.SerializeObject(resetPassword);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"{SD.AccountApiPath}ResetPassword", bodyContent);
        var contentTemp = await response.Content.ReadAsStringAsync();
        //var result = JsonConvert.DeserializeObject<bool>(contentTemp);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<bool> ChangePassword(ChangePasswordDTO changePasswordDTO, string token)
    {
        var content = JsonConvert.SerializeObject(changePasswordDTO);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"{SD.AccountApiPath}ChangePassword", bodyContent);
        var contentTemp = await response.Content.ReadAsStringAsync();
        //var result = JsonConvert.DeserializeObject<bool>(contentTemp);

        if (token != null && token.Length != 0)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        if (response.IsSuccessStatusCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
