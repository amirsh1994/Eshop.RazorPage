using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Sellers;
using Eshop.RazorPage.Models.Sellers.Commands;

namespace Eshop.RazorPage.Services.Sellers;

public interface ISellerService
{
    Task<ApiResult?> CreateSeller(CreateSellerCommand command);

    Task<ApiResult?> EditSeller(EditSellerCommand command);

    Task<ApiResult> EditSellerInventory(EditSellerInventoryCommand command);

    Task<ApiResult> AddSellerInventory(AddSellerInventoryCommand command);

    Task<SellerFilterResult?> GetSellersByFilter(SellerFilterParams filterParams);

    Task<SellerDto?> GetCurrent();

    Task<SellerDto?> GetSellerById(long sellerId);

    Task<List<InventoryDto>> GetInventories();

    Task<InventoryDto?> GetSellerInventoryById(long inventoryId);

}


public class SellerService(HttpClient client) : ISellerService
{
    private const string ModuleName = "seller";
    public async Task<ApiResult?> CreateSeller(CreateSellerCommand command)
    {
        var result = await client.PostAsJsonAsync(ModuleName, command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> EditSeller(EditSellerCommand command)
    {
        var result = await client.PutAsJsonAsync(ModuleName, command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> EditSellerInventory(EditSellerInventoryCommand command)
    {
        var result = await client.PutAsJsonAsync($"{ModuleName}/Inventory", command);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> AddSellerInventory(AddSellerInventoryCommand command)
    {
        var result = await client.PostAsJsonAsync($"{ModuleName}/Inventory", command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<SellerFilterResult?> GetSellersByFilter(SellerFilterParams filterParams)
    {
        var url = $"{filterParams.GenerateBaseFilterUrl(ModuleName)}" +
                  $"&NationalCode={filterParams.NationalCode}&ShopName={filterParams.ShopName}";
        var result = await client.GetFromJsonAsync<ApiResult<SellerFilterResult>>(url);
        return result?.Data;
    }

    public async Task<SellerDto?> GetCurrent()
    {
        var result = await client.GetFromJsonAsync<ApiResult<SellerDto>>($"{ModuleName}/current");
        return result?.Data;
    }

    public async Task<SellerDto?> GetSellerById(long sellerId)
    {
        var result = await client.GetFromJsonAsync<ApiResult<SellerDto>>($"{ModuleName}/{sellerId}");
        return result?.Data;
    }

    public async Task<List<InventoryDto>> GetInventories()
    {
        var result = await client.GetFromJsonAsync<ApiResult<List<InventoryDto>>>($"{ModuleName}/inventory");
        return result.Data;
    }

    public async Task<InventoryDto?> GetSellerInventoryById(long inventoryId)
    {
        var result = await client.GetFromJsonAsync<ApiResult<InventoryDto>>($"{ModuleName}/Inventory/{inventoryId}");
        return result?.Data;
    }
}