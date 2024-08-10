using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.UserAddress;

namespace Eshop.RazorPage.Services.UserAddress;

public interface IUserAddressService
{
    Task<ApiResult?> CreateUserAddress(CreateUserAddressCommand command);

    Task<ApiResult?> EditUserAddress(EditUserAddressCommand command);

    Task<ApiResult?> DeleteUserAddress(long userAddressId);

    Task<List<AddressDto>?> GetUserAddresses();

    Task<AddressDto?> GetUserAddressById(long addressId);

    Task<ApiResult?> SetActiveUserAddress(long addressId);

}


public class UserAddressService(HttpClient client) : IUserAddressService
{
    private const string ModuleName = "UserAddress";
    public async Task<ApiResult?> CreateUserAddress(CreateUserAddressCommand command)
    {
        var result = await client.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> EditUserAddress(EditUserAddressCommand command)
    {
        var result = await client.PutAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> DeleteUserAddress(long userAddressId)
    {
        var result = await client.DeleteAsync($"{ModuleName}/{userAddressId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<List<AddressDto>?> GetUserAddresses()
    {
        var result = await client.GetFromJsonAsync<ApiResult<List<AddressDto>?>>(ModuleName);
        return result?.Data;
    }

    public async Task<AddressDto?> GetUserAddressById(long addressId)
    {
        var result = await client.GetFromJsonAsync<ApiResult<AddressDto?>>($"{ModuleName}/{addressId}");
        return result.Data;
    }

    public async Task<ApiResult?> SetActiveUserAddress(long addressId)
    {
        var result = await client.PutAsync($"{ModuleName}/SetActiveAddress/{addressId}",null);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }
}