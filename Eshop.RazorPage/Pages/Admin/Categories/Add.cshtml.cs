using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Categories;
using Eshop.RazorPage.Services.Categories;
using Eshop.RazorPage.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Admin.Categories;

[BindProperties]
public class AddModel(ICategoryService categoryService) : BaseRazorPage
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }


    [Display(Name = "slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }

    public SeoDataViewModel SeoData { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(long? parentId)
    {
        if (parentId == null)
        {
            var result = await categoryService.CreateCategory(new CreateCategoryCommand
            {
                Title = Title,
                SeoData = SeoData.MapToSeoData(),
                Slug = Slug
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
        else
        {
            var result = await categoryService.AddChildCategory(new AddChildCommand
            {
                ParentId =(long)parentId,
                Title = Title,
                SeoData = SeoData.MapToSeoData(),
                Slug = Slug
            });
            return RedirectAndShowAlert(result, RedirectToPage("Index"));
        }
    }
}

