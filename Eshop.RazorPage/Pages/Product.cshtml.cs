using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Models.Sellers;
using Eshop.RazorPage.Services.Products;
using Eshop.RazorPage.Services.Sellers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages;

[BindProperties]
public class ProductModel(IProductService productService, ISellerService sellerService) : PageModel
{
    public SingleProductDto productModel { get; set; }

    public async Task<IActionResult> OnGet(string slug)
    {
        var product = await productService.GetSingleProduct(slug);
        if (product == null)
            return NotFound();

        productModel = product;

        return Page();
    }
}

