using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Banners;

namespace Eshop.RazorPage.Services.Banners;

public interface IBannerService
{
    Task<BannerDto?> GetBannerById(long bannerId);

    Task<List<BannerDto>?> GetBannerList();

    Task<ApiResult?> CreateBanner(CreateBannerCommand command);

    Task<ApiResult?> EditBanner(EditBannerCommand command);

    Task<ApiResult?> Delete(long bannerId);
}


public class BannerService(HttpClient client) : IBannerService
{
    public async Task<BannerDto?> GetBannerById(long bannerId)
    {
        var result = await client.GetFromJsonAsync<ApiResult<BannerDto>>($"banner/{bannerId}");
        return result?.Data;
    }

    public async Task<List<BannerDto>?> GetBannerList()
    {
        var result = await client.GetFromJsonAsync<ApiResult<List<BannerDto>>>("banner");
        return result == null ? [] : result.Data;
    }

    public async Task<ApiResult?> CreateBanner(CreateBannerCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Link), "Link");
        formData.Add(new StringContent(command.Positions.ToString()), "Positions");
        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "Positions");
        var result = await client.PostAsync("banner", formData);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> EditBanner(EditBannerCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Link), "Link");
        formData.Add(new StringContent(command.Positions.ToString()), "Positions");
        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "Positions");
        formData.Add(new StringContent(command.BannerId.ToString()), "Id");
        var result = await client.PutAsync("banner", formData);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> Delete(long bannerId)
    {
        var result = await client.DeleteFromJsonAsync<ApiResult>($"banner/{bannerId}");
        return result;
    }
}