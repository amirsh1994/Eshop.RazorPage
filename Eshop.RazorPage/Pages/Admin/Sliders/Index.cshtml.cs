using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Sliders;
using Eshop.RazorPage.Services.Sliders;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Admin.Sliders;


public class IndexModel(ISliderService sliderService) : BaseRazorPage
{
    public List<SliderDto> Sliders { get; set; }

    public async Task OnGet()
    {
        var result = await sliderService.GetSliders();
        Sliders = result;

    }

    public async Task<IActionResult> OnPostDeleteSlider(long sliderId)
    {
        return await AjaxTryCatch(() =>
        {
            return sliderService.DeleteSlider(sliderId);
        });
    }
}

