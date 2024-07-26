using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Categories;

namespace Eshop.RazorPage.Services.Categories;

public interface ICategoryService
{
    Task<ApiResult?> CreateCategory(CreateCategoryCommand command);

    Task<ApiResult?> EditCategoryCommand(EditCategoryCommand command);

    Task<ApiResult?> DeleteCategory(long categoryId);

    Task<ApiResult?> AddChildCategory(AddChildCommand command);

    Task<CategoryDto?> GetCategoryById(long categoryId);

    Task<List<CategoryDto>?> GetCategories();

    Task<List<ChildCategoryDto>?> GetChildByParentId(long parentId);



}

public class CategoryService(HttpClient client) : ICategoryService
{
    public async Task<ApiResult?> CreateCategory(CreateCategoryCommand command)
    {
        var result = await client.PostAsJsonAsync("category", command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> EditCategoryCommand(EditCategoryCommand command)
    {
        var result = await client.PutAsJsonAsync("category", command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> DeleteCategory(long categoryId)
    {
        var result = await client.DeleteAsync($"category/{categoryId}");
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> AddChildCategory(AddChildCommand command)
    {
        var result = await client.PostAsJsonAsync("category/addChild", command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<CategoryDto?> GetCategoryById(long categoryId)
    {
        var result = await client.GetFromJsonAsync<ApiResult<CategoryDto>>($"category/{categoryId}");
        return result?.Data;
    }

    public async Task<List<CategoryDto>?> GetCategories()
    {
        var result = await client.GetFromJsonAsync<ApiResult<List<CategoryDto>>>("category");
        return result?.Data;
    }

    public async Task<List<ChildCategoryDto>?> GetChildByParentId(long parentId)
    {
        var result = await client.GetFromJsonAsync<ApiResult<List<ChildCategoryDto>>>($"category/getChild/{parentId}");
        return result?.Data;
    }
}