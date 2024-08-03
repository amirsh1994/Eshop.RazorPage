using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Auth;

    public class LogoutModel(IAuthService authService) : BaseRazorPage
    {
        public async Task<IActionResult> OnGet()
        {
            var result = await authService.LogOut();

            return RedirectAndShowAlert(result, RedirectToPage("../Index"));
        }
    }

