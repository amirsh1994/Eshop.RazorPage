using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Roles;

namespace Eshop.RazorPage.Services.Roles;

public interface IRoleService
{
    Task<ApiResult?> CreateRole(CreateRoleRoleCommand command);

    Task<ApiResult?> EditRole(EditRoleCommand command);

    Task<RoleDto?> GetRoleById(long roleId);

    Task<List<RoleDto>> GetRoles();

}


public class RoleService(HttpClient client):IRoleService
{
    public async Task<ApiResult?> CreateRole(CreateRoleRoleCommand command)
    {
        var result = await client.PostAsJsonAsync("Role", command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> EditRole(EditRoleCommand command)
    {
        var result = await client.PutAsJsonAsync("Role", command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<RoleDto?> GetRoleById(long roleId)
    {
        var result = await client.GetFromJsonAsync<ApiResult<RoleDto>>($"Role/{roleId}");
        return result?.Data;
    }

    public async Task<List<RoleDto>?> GetRoles()
    {
        var result = await client.GetFromJsonAsync<ApiResult<List<RoleDto>>>("Role");
        return result?.Data;
    }
}