using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Auth;

    public class LogoutModel(IAuthService authService) : BaseRazorPage
    {
    public async Task<IActionResult> OnGet()
    {
        var result = await authService.LogOut();

        if (result.IsSuccess)
        {
            HttpContext.Response.Cookies.Delete("token");
            HttpContext.Response.Cookies.Delete("refresh-token");
        }
        return RedirectAndShowAlert(result, RedirectToPage("../Index"));
    }
}

