using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.ShippingMethods;

namespace Eshop.RazorPage.Services.ShippingMethods;

public interface IShippingMethodeService
{
    Task<List<ShippingMethodDto>> GetShippingMethods();
}


public class ShippingMethodeService(HttpClient client):IShippingMethodeService
{
    private const string ModuleName = "ShippingMethod";

    public async Task<List<ShippingMethodDto>> GetShippingMethods()
    {
        var result = await client.GetFromJsonAsync<ApiResult<List<ShippingMethodDto>>>("ShippingMethod");
        return result?.Data ?? [];
    }
}