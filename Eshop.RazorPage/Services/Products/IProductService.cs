using System.Net.Http.Json;
using System.Text;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Models.Products.Commands;
using Eshop.RazorPage.Models.Products.ProductShop;
using Newtonsoft.Json;

namespace Eshop.RazorPage.Services.Products;

public interface IProductService
{
    Task<ApiResult?> CreateProduct(CreateProductCommand command);

    Task<ApiResult?> EditProduct(EditProductCommand command);

    Task<ApiResult?> AddProductImage(AddProductImageCommand command);

    Task<ApiResult?> DeleteProductImage(DeleteProductImageCommand command);

    Task<ProductFilterResult> GetProductByFilter(ProductFilterParams filterParams);

    Task<ProductShopResult?> GetProductForShop(ProductShopFilterParam filterParam);

    Task<ProductDto?> GetProductById(long productId);

    Task<ProductDto?> GetProductBySlug(string slug);

}


public class ProductService(HttpClient client) : IProductService
{
    private const string ModuleName = "product";
    public async Task<ApiResult?> CreateProduct(CreateProductCommand command)
    {
        var specification = JsonConvert.SerializeObject(command.Specifications);
        var formData = new MultipartFormDataContent();
        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);
        formData.Add(new StringContent(command.Title), "Title");
        formData.Add(new StringContent(command.Slug), "Slug");
        formData.Add(new StringContent(command.Description), "Description");
        formData.Add(new StringContent(command.CategoryId.ToString()), "CategoryId");
        formData.Add(new StringContent(command.SubCategoryId.ToString()), "SubCategoryId");
        if (command.FirstSubCategoryId != null)
            formData.Add(new StringContent(command.FirstSubCategoryId.ToString() ?? string.Empty), "FirstSubCategoryId");

        formData.Add(new StringContent(command.SeoData.Canonical), "SeoData.Canonical");
        formData.Add(new StringContent(command.SeoData.MetaDescription), "SeoData.MetaDescription");
        formData.Add(new StringContent(command.SeoData.MetaKeyWords), "SeoData.MetaKeyWords");
        formData.Add(new StringContent(command.SeoData.MetaTitle), "SeoData.MetaTitle");
        formData.Add(new StringContent(command.SeoData.Schema), "SeoData.Schema");
        formData.Add(new StringContent(command.SeoData.IndexPage.ToString()), "SeoData.IndexPage");
        formData.Add(new StringContent(specification, Encoding.UTF8, "application/json"), "Specifications");

        var result = await client.PostAsync(ModuleName, formData);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;

    }

    public async Task<ApiResult?> EditProduct(EditProductCommand command)
    {
        var specification = JsonConvert.SerializeObject(command.Specifications);
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(command.Id.ToString()), "Id");
        if (command.ImageFile != null)
            formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);

        formData.Add(new StringContent(command.Title), "Title");
        formData.Add(new StringContent(command.Slug), "Slug");
        formData.Add(new StringContent(command.Description), "Description");
        formData.Add(new StringContent(command.CategoryId.ToString()), "CategoryId");
        formData.Add(new StringContent(command.SubCategoryId.ToString()), "SubCategoryId");
        if (command.FirstSubCategoryId != null)
            formData.Add(new StringContent(command.FirstSubCategoryId.ToString() ?? string.Empty), "FirstSubCategoryId");

        formData.Add(new StringContent(command.SeoData.Canonical), "SeoData.Canonical");
        formData.Add(new StringContent(command.SeoData.MetaDescription), "SeoData.MetaDescription");
        formData.Add(new StringContent(command.SeoData.MetaKeyWords), "SeoData.MetaKeyWords");
        formData.Add(new StringContent(command.SeoData.MetaTitle), "SeoData.MetaTitle");
        formData.Add(new StringContent(command.SeoData.Schema), "SeoData.Schema");
        formData.Add(new StringContent(command.SeoData.IndexPage.ToString()), "SeoData.IndexPage");
        formData.Add(new StringContent(specification, Encoding.UTF8, "application/json"), "Specifications");

        var result = await client.PutAsync(ModuleName, formData);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> AddProductImage(AddProductImageCommand command)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StreamContent(command.ImageFile.OpenReadStream()), "ImageFile", command.ImageFile.FileName);
        formData.Add(new StringContent(command.ProductId.ToString()), "ProductId");
        formData.Add(new StringContent(command.Sequence.ToString()), "Sequence");
        var result = await client.PostAsync($"{ModuleName}/images", formData);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> DeleteProductImage(DeleteProductImageCommand command)
    {
        var json = JsonConvert.SerializeObject(command);
        var message = new HttpRequestMessage
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
            Method = HttpMethod.Delete,
            RequestUri = new Uri($"{ModuleName}/images"),
        };
        var result = await client.SendAsync(message);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;

    }

    public async Task<ProductFilterResult?> GetProductByFilter(ProductFilterParams filterParams)
    {
        var url = $"{ModuleName}?pageId={filterParams.PageId}&take={filterParams.Take}" +
                        $"&Slug={filterParams.Slug}&Title={filterParams.Title}";
        if (filterParams.Id != null) url += $"&Id={filterParams.Id}";
        var result = await client.GetFromJsonAsync<ApiResult<ProductFilterResult>>(url);
        var response = result?.Data;
        return response;
    }

    public async Task<ProductShopResult?> GetProductForShop(ProductShopFilterParam filterParam)
    {
        var url = $"{ModuleName}/shop?pageId={filterParam.PageId}&take={filterParam.Take}" +
                  $"&CategorySlug{filterParam.CategorySlug}&OnlyAvailableProducts={filterParam.OnlyAvailableProducts}" +
                  $"&Search={filterParam.Search}&SearchOrderBy={filterParam.SearchOrderBy}";
        if (filterParam.JustHasDiscount != null)
            url += $"&JustHasDiscount={filterParam.JustHasDiscount}";
        var result = await client.GetFromJsonAsync<ApiResult<ProductShopResult>>(url);
        return result?.Data;
    }

    public async Task<ProductDto?> GetProductById(long productId)
    {
        var result = await client.GetFromJsonAsync<ApiResult<ProductDto>>($"{ModuleName}/{productId}");
        return result?.Data;
    }

    public async Task<ProductDto?> GetProductBySlug(string slug)
    {
        var result = await client.GetFromJsonAsync<ApiResult<ProductDto>>($"{ModuleName}/bySlug/{slug}");
        return result?.Data;
    }
}