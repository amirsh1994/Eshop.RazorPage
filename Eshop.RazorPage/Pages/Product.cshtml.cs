using Eshop.RazorPage.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages;

    public class ProductModel(IProductService productService) : PageModel
    {
        public void OnGet()
        {
        }
    }

