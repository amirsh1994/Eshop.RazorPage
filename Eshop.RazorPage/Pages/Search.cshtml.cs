using Eshop.RazorPage.Models.Products.ProductShop;
using Eshop.RazorPage.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages;

public class SearchModel(IProductService productService) : PageModel
{
    [BindProperty]
    public ProductShopResult Result { get; set; } = new();

    public async Task OnGet(string q = "", int pageId = 1, string category = "", bool? haveDiscount = null, bool justAvailableProducts = true)
    {
        var result = await productService.GetProductForShop(new ProductShopFilterParam
        {
            PageId = pageId,
            Take = 18,
            CategorySlug = category,
            Search = q,
            OnlyAvailableProducts = justAvailableProducts,
            JustHasDiscount = haveDiscount,
            SearchOrderBy = ProductSearchOrderBy.Latest
        });
        Result = result;
    }
}

