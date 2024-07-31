using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Models.Auth;
using Eshop.RazorPage.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Auth;

[BindProperties]
[ValidateAntiForgeryToken]
public class RegisterModel(IAuthService service) : PageModel
{
    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "حدقل 11 کاراکتر")]
    public string PhoneNumber { get; set; }

    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Password { get; set; }

    [Display(Name = "تکرار کلمه عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Compare(nameof(Password), ErrorMessage = "کلمه‌های عبور یکسان نیستند")]
    public string ConfirmedPassword { get; set; }

    public void OnGet()
    {

    }


    public async Task<IActionResult> OnPost()
    {
      var result= await service.Register(new RegisterCommand
        {
            PhoneNumber = PhoneNumber,
            Password = Password,
            ConfirmPassword = ConfirmedPassword
        });
        if (result?.IsSuccess==false)
        {
            ModelState.AddModelError(nameof(PhoneNumber),result.MetaData.Message);
            return Page();
        }
        return RedirectToPage("Login");

    }
}


