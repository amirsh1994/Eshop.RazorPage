using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Banners;
using Eshop.RazorPage.Services.Banners;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Banners;

[BindProperties]
public class IndexModel(IBannerService bannerService,IRenderViewToString renderViewToString) : BaseRazorPage
{
    public List<BannerDto> Banners { get; set; }

    public async Task OnGet()
    {

        Banners = await bannerService.GetBannerList();
    }

    public async Task<IActionResult> OnGetRenderAddPage()
    {
        return await AjaxTryCatch(async () =>
        {
            var view = await renderViewToString.RenderToStringAsync("_Add",new CreateBannerCommand(),PageContext);
            return ApiResult<string>.Success(view);
        } );
    }


    public async Task<IActionResult> OnGetRenderEditPage(long id)
    {
        return await AjaxTryCatch(async () =>
        {
            var banner = await bannerService.GetBannerById(id);

            if (banner == null)
            {
                return ApiResult<string>.Error();
            }
            
            var model = new EditBannerCommand
            {
                BannerId = banner.Id,
                Link = banner.Link,
                Positions = banner.Positions
            };
            var view = await renderViewToString.RenderToStringAsync("_Edit", model, PageContext);
            return ApiResult<string>.Success(view);
        });
    }


    public async Task<IActionResult> OnPostCreateBanner(CreateBannerCommand command)
    {
        return await AjaxTryCatch(async () => await bannerService.CreateBanner(command));
    }


    public async Task<IActionResult> OnPostEditBanner(EditBannerCommand command)
    {

        return await AjaxTryCatch(async () =>
        {
            return await bannerService.EditBanner(command);
        });
    }


    public async Task<IActionResult> OnPostDelete(long id)
    {
        return await AjaxTryCatch(async () => await bannerService.Delete(id));
    }
}

