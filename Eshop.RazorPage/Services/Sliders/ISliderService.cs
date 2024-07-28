using Eshop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Sliders;

namespace Eshop.RazorPage.Services.Sliders;

public interface ISliderService
{
    Task<ApiResult?> CreateSlider(CreateSliderCommand command);

    Task<ApiResult?> EditSlider(EditSliderCommand command);

    Task<ApiResult?> DeleteSlider(long sliderId);

    Task<List<SliderDto>?> GetSliders();

    Task<SliderDto?> GetSliderById(long id);




}



public class SliderService(HttpClient client) : ISliderService
{
    private const string ModuleName = "slider";
    public async Task<ApiResult?> CreateSlider(CreateSliderCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Title), "Title");
        formData.Add(new StringContent(command.Link), "Title");
        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);
        var result = await client.PostAsync($"{ModuleName}", formData);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;


    }

    public async Task<ApiResult?> EditSlider(EditSliderCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Title), "Title");
        formData.Add(new StringContent(command.SliderId.ToString()), "SliderId");
        formData.Add(new StringContent(command.Link), "Title");
        if (command.ImageFile != null && command.ImageFile.IsImage())
            formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);
        var result = await client.PutAsync($"{ModuleName}", formData);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> DeleteSlider(long sliderId)
    {
        var result = await client.DeleteAsync($"{ModuleName}/{sliderId}");
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<List<SliderDto>?> GetSliders()
    {
        var result = await client.GetFromJsonAsync<ApiResult<List<SliderDto>>>(ModuleName);
        return result?.Data;
    }

    public async Task<SliderDto?> GetSliderById(long id)
    {
        var result = await client.GetFromJsonAsync<ApiResult<SliderDto>>($"{ModuleName}/{id}");
        return result?.Data;
    }
}