using Eshop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Eshop.RazorPage.Services.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Sliders;

namespace Eshop.RazorPage.Pages.Admin.Sliders;

[BindProperties]
public class EditModel(ISliderService sliderService) : BaseRazorPage
{

    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Url)]
    public string Link { get; set; }


    [Display(Name = "عکس اسلایدر")]
    [FileImage(ErrorMessage = "عکس نامعتبر می باشد")]
    public IFormFile? ImageFileName { get; set; }


    [Display(Name = "عنوان ")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

    public string ImageName { get; set; }

    public async Task<IActionResult> OnGet(long id)
    {
        var slider = await sliderService.GetSliderById(id);
        if (slider == null)
        {
            return RedirectToPage("Index");
        }

        Title = slider.Title;
        Link = slider.Link;
        ImageName = slider.ImageName;
        return Page();

    }

    public async Task<IActionResult> OnPost(long id)
    {
        var result = await sliderService.EditSlider(new EditSliderCommand
        {
            SliderId = id,
            Title = Title,
            Link = Link,
            ImageFile = ImageFileName
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }


}

