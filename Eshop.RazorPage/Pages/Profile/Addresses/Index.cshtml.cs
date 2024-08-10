using AutoMapper;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.UserAddress;
using Eshop.RazorPage.Services.UserAddress;
using Eshop.RazorPage.ViewModels.Users.Addresses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Profile.Addresses;

[BindProperties]
[Authorize]
public class IndexModel(IUserAddressService userAddressService,IRenderViewToString renderViewToString,IMapper mapper) : BaseRazorPage
{
    public List<AddressDto>? Addresses { get; set; }

    public async Task OnGet()
    {
        Addresses = await userAddressService.GetUserAddresses();

    }

    public async Task<IActionResult> OnPostAsync(long addressId)
    {
        var result = await userAddressService.DeleteUserAddress(addressId);
        return RedirectAndShowAlert(result,RedirectToPage("Index"),RedirectToPage("Index"));
    }

    public async Task<IActionResult> OnGetShowAddPage()
    {
        var view = await renderViewToString.RenderToStringAsync("_Add",new CreateUserAddressViewModel(),PageContext);
        var result= await AjaxTryCatch(async () => ApiResult<string>.Success(view));
        return result;
    }

    public async Task<IActionResult> OnGetShowEditPage(long addressId)
    {
            var address = await userAddressService.GetUserAddressById(addressId);
            var model = mapper.Map<EditUserAddressViewModel>(address);
            var view = await renderViewToString.RenderToStringAsync("_Edit", model, PageContext);
            var result =await AjaxTryCatch(async () => ApiResult<string>.Success(view));
            return result;
    }

    public async Task<IActionResult> OnPostAddAddress(CreateUserAddressViewModel viewModel)
    {
      
       
        return await AjaxTryCatch(async () =>
        {
            var command = mapper.Map<CreateUserAddressCommand>(viewModel);
            var result = await userAddressService.CreateUserAddress(command);
            return result;
        },true);
    }

    public async Task<IActionResult> OnPostEditAddress(EditUserAddressViewModel viewModel)
    {
        return await AjaxTryCatch(async () =>
        {
            var command = mapper.Map<EditUserAddressCommand>(viewModel);
            var apiResult = await userAddressService.EditUserAddress(command);
            return apiResult;
        },true );
    }

    public async Task<IActionResult> OnGetSetActiveAddress(long addressId)
    {
        return await AjaxTryCatch(async () =>
        {
            var result = await userAddressService.SetActiveUserAddress(addressId);

            return result;
        },true);
    }
}

