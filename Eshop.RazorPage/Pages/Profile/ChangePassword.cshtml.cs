using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Users.Commands;
using Eshop.RazorPage.Services.Users;

namespace Eshop.RazorPage.Pages.Profile;

[BindProperties]
public class ChangePasswordModel(IUserService userService) : BaseRazorPage
{
    [Display(Name = "کلمه عبور فعلی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }




    [Display(Name = "کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MinLength(6, ErrorMessage = "کلمه عبور جدید باید بیشتر از5 کاراکتر باشد")]
    [DataType(DataType.Password)]
    public string Password { get; set; }



    [Display(Name = "تکرار کلمه عبور جدید")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Compare(nameof(Password), ErrorMessage = "کلمه های عبور یکسان نمی باشد ")]
    [DataType(DataType.Password)]
    public string ConfirmedPassword { get; set; }



    public void OnGet()
    {
    }


    public async Task<IActionResult> OnPost()
    {
        var result = await userService.ChangePassword(new ChangePasswordCommand
        {
            CurrentPassword = CurrentPassword,
            Password = Password,
            ConfirmedPassword = ConfirmedPassword
        });
        return RedirectAndShowAlert(result,RedirectToPage("Index"));
    }
}

