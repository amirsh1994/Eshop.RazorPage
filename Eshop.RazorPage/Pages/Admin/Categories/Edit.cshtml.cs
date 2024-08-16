using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Categories;
using Eshop.RazorPage.Services.Categories;
using Eshop.RazorPage.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Categories;

[BindProperties]
public class EditModel(ICategoryService categoryService) : BaseRazorPage
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }


    [Display(Name = "slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }

    public SeoDataViewModel SeoData { get; set; }


    public async Task<IActionResult> OnGet(long id)
    {
        var cat = await categoryService.GetCategoryById(id);
        if (cat == null)
        {
            return RedirectToPage("Index");
        }

        Title = cat.Title;
        Slug = cat.Slug;
        SeoData = SeoDataViewModel.MapSeoDataToViewModel(cat.SeoData);
        return Page();

    }


    public async Task<IActionResult> OnPost(long id)
    {
        var result = await categoryService.EditCategoryCommand(new EditCategoryCommand
        {
            Id = id,
            Title = Title,
            Slug = Slug,
            SeoData = SeoData.MapToSeoData()
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

