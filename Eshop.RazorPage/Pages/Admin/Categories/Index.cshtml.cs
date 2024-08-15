using Eshop.RazorPage.Models.Categories;
using Eshop.RazorPage.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Categories;

[BindProperties]
public class IndexModel(ICategoryService categoryService) : PageModel
{
    public List<CategoryDto> Categories { get; set; }
    public async Task OnGet()
    {
         Categories = await categoryService.GetCategories();

    }
}

