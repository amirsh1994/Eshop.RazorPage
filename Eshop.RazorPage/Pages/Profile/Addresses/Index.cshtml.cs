using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.UserAddress;
using Eshop.RazorPage.Services.UserAddress;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Profile.Addresses;

[BindProperties]
public class IndexModel(IUserAddressService userAddressService,IRenderViewToString renderViewToString) : BaseRazorPage
{
    public List<AddressDto>? Addresses { get; set; }

    public async Task OnGet()
    {
        Addresses = await userAddressService.GetUserAddresses();

    }

    public async Task<IActionResult> OnGetShowPage()
    {
        var view = await renderViewToString.RenderToStringAsync("_Add",new CreateUserAddressCommand(),PageContext);
        return await AjaxTryCatch(async () => ApiResult<string>.Success(view));
    }
}

