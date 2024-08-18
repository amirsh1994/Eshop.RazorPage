using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Services.Categories;
using Eshop.RazorPage.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Admin.Products;



public class IndexModel(IProductService productService,ICategoryService categoryService) : BaseRazorFilter<ProductFilterParams>
{
    public ProductFilterResult FilterResult { get; set; }

    public async Task OnGet()
    {
        FilterResult= await productService.GetProductByFilter(FilterParam);
    }

    public async Task<IActionResult> OnGetLoadChildrenCategories(long parentId)
    {
        var options = "<option value='0'>انتخاب کنید</option>";
        var children = await categoryService.GetChildByParentId(parentId);
        children.ForEach(x =>
        {
            options += $"<option value={x.Id}>{x.Title}</option>";
        });
        
        return Content(options);
    }
}

