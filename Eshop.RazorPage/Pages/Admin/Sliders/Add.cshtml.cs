using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Eshop.RazorPage.Models.Sliders;
using Eshop.RazorPage.Services.Sliders;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Admin.Sliders;

[BindProperties]
public class AddModel(ISliderService sliderService) : BaseRazorPage
{
    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Url)]
    public string Link { get; set; }


    [Display(Name = "عکس اسلایدر")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileImage(ErrorMessage = "عکس نامعتبر می باشد")]
    public IFormFile ImageFileName { get; set; }


    [Display(Name = "عنوان ")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    public void OnGet()
    {
    }


    public async Task<IActionResult> OnPost()
    {
        var result = await sliderService.CreateSlider(new CreateSliderCommand
        {
            Link = Link,
            ImageFile = ImageFileName,
            Title = Title
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

