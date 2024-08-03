using System.Net;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Auth;

namespace Eshop.RazorPage.Services.Auth;

public interface IAuthService
{
    Task<ApiResult<LoginResponse>?> Login(LoginCommand command);

    Task<ApiResult?> Register(RegisterCommand command);

    Task<ApiResult<LoginResponse>?> RefreshToken();

    Task<ApiResult?> LogOut();


}


public class AuthService(HttpClient client,IHttpContextAccessor accessor) : IAuthService
{
    public async Task<ApiResult<LoginResponse>?> Login(LoginCommand command)
    {
      var result=  await client.PostAsJsonAsync("auth/login", command);
      var response=await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>();
      return response;
    }

    public async Task<ApiResult?> Register(RegisterCommand command)
    {
        var result = await client.PostAsJsonAsync("auth/register", command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult<LoginResponse>?> RefreshToken()
    {
        var refreshToken = accessor.HttpContext.Request.Cookies["refreshToken"];
        var result = await client.PostAsync($"Auth/RefreshToken?refreshToken={refreshToken}",null);
        var response =await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>();
        return response;
    }

    public async Task<ApiResult?> LogOut()
    {
        try
        {
            var result = await client.DeleteAsync("auth/logout");
            var response = await result.Content.ReadFromJsonAsync<ApiResult>();
            return response;
        }
        catch (Exception ex)
        {

            return ApiResult.Error();
        }
    }
}