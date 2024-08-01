using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Models.Auth;
using Eshop.RazorPage.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Auth;
[BindProperties]
[ValidateAntiForgeryToken]
public class LoginModel (IAuthService authService): PageModel
{
    #region Models


    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PhoneNumber { get; set; }



    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    #endregion




    #region Get

    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            return Redirect("/");
        }

        return Page();
    }

    #endregion

    #region Post

    public async Task<IActionResult> OnPost()
    {
        var result = await authService.Login(new LoginCommand
        {
            phoneNumber = PhoneNumber,
            Password = Password
        });
        if (result?.IsSuccess == false)
        {
            ModelState.AddModelError(nameof(PhoneNumber),result.MetaData.Message);
            return Page();
        }

        var token = result?.Data.Token;
        var refreshToken = result?.Data.RefreshToken;
        if (token != null) HttpContext.Response.Cookies.Append("token", token);
        if (refreshToken != null) HttpContext.Response.Cookies.Append("refresh-token", refreshToken);

        return Redirect("/");

    }

    #endregion




}

