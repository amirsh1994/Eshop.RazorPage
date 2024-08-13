using Eshop.RazorPage.Models.Sliders;
using Eshop.RazorPage.Services.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Sliders;

public class IndexModel(ISliderService sliderService) : PageModel
{
    public List<SliderDto> Sliders { get; set; }

    public async Task OnGet()
    {
        var result = await sliderService.GetSliders();
        Sliders = result;

    }
}

