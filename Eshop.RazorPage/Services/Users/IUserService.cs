using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Users;
using Eshop.RazorPage.Models.Users.Commands;


namespace Eshop.RazorPage.Services.Users;

public interface IUserService
{
    Task<ApiResult?> CreateUser(CreateUserCommand command);

    Task<ApiResult?> EditUser(EditUserCommand command);

    Task<ApiResult?> EditUserCurrent(EditUserCommand command);

    Task<ApiResult?> ChangePassword(ChangePasswordCommand command);

    Task<UserFilterResult?> GetUsersByFilter(UserFilterParams filterParams);

    Task<UserDto?> GetUserById(long userId);

    Task<UserDto?> GetUserByPhoneNumber(string phoneNumber);

    Task<UserDto?> GetCurrentUser();





}



public class UserService(HttpClient client) : IUserService
{
    private const string ModuleName = "User";
    public async Task<ApiResult?> CreateUser(CreateUserCommand command)
    {
        var result = await client.PostAsJsonAsync(ModuleName, command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> EditUser(EditUserCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Email), "Email");
        formData.Add(new StringContent(command.Family), "Family");
        formData.Add(new StringContent(command.Name), "Name");
        formData.Add(new StringContent(command.Password), "Password");
        formData.Add(new StringContent(command.PhoneNumber), "PhoneNumber");
        formData.Add(new StringContent(command.Gender.ToString()), "Gender");
        formData.Add(new StringContent(command.UserId.ToString()), "UserId");
        formData.Add(new StreamContent(command.Avatar.OpenReadStream()), "Avatar", command.Avatar.FileName);
        var result = await client.PostAsync(ModuleName, formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> EditUserCurrent(EditUserCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Email), "Email");
        formData.Add(new StringContent(command.Family), "Family");
        formData.Add(new StringContent(command.Name), "Name");
        formData.Add(new StringContent(command.Password), "Password");
        formData.Add(new StringContent(command.PhoneNumber), "PhoneNumber");
        formData.Add(new StringContent(command.Gender.ToString()), "Gender");
        formData.Add(new StringContent(command.UserId.ToString()), "UserId");
        if (command.Avatar != null)
            formData.Add(new StreamContent(command.Avatar.OpenReadStream()), "Avatar", command.Avatar.FileName);

        var result = await client.PostAsync($"{ModuleName}/current", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>();

    }

    public async Task<ApiResult?> ChangePassword(ChangePasswordCommand command)
    {
        try
        {
            var result = await client.PutAsJsonAsync($"{ModuleName}/changePassword", command);
            var response = await result.Content.ReadFromJsonAsync<ApiResult>();
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<UserFilterResult?> GetUsersByFilter(UserFilterParams filterParams)
    {
        var url = $"{filterParams.GenerateBaseFilterUrl(ModuleName)}" +
                  $"&Email={filterParams.Email}&PhoneNumber={filterParams.PhoneNumber}&Id={filterParams.Id}";
        var result = await client.GetFromJsonAsync<ApiResult<UserFilterResult>>(url);
        return result?.Data;
    }

    public async Task<UserDto?> GetUserById(long userId)
    {
        var result = await client.GetFromJsonAsync<ApiResult<UserDto>>($"{ModuleName}/{userId}");
        return result?.Data;
    }

    public async Task<UserDto?> GetUserByPhoneNumber(string phoneNumber)
    {
        var result = await client.GetFromJsonAsync<ApiResult<UserDto>>($"{ModuleName}/{phoneNumber}");
        return result?.Data;
    }

    public async Task<UserDto?> GetCurrentUser()
    {
        var result = await client.GetFromJsonAsync<ApiResult<UserDto>>($"{ModuleName}/current");
        return result?.Data;
    }
}