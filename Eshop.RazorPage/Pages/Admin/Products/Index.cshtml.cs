using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Services.Products;

namespace Eshop.RazorPage.Pages.Admin.Products;



public class IndexModel(IProductService productService) : BaseRazorFilter<ProductFilterParams>
{
    public ProductFilterResult FilterResult { get; set; }
    public async Task OnGet()
    {
        FilterResult= await productService.GetProductByFilter(FilterParam);
    }
}

