using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Categories;
using Eshop.RazorPage.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Categories;

[BindProperties]
public class IndexModel(ICategoryService categoryService) : BaseRazorPage
{
    public List<CategoryDto> Categories { get; set; }

    public async Task OnGet()
    {
         Categories = await categoryService.GetCategories();

    } 

    public async Task<IActionResult> OnPostDelete(long id)
    {
        return await AjaxTryCatch(async () => await categoryService.DeleteCategory(id));
    }
}

