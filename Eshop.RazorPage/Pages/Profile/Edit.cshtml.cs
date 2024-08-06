using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Eshop.RazorPage.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Models.Users.Commands;
using Eshop.RazorPage.Services.Users;
using Microsoft.AspNetCore.Authorization;

namespace Eshop.RazorPage.Pages.Profile;



[BindProperties]
[Authorize]
public class EditModel(IUserService userService) : BaseRazorPage
{
    #region Model

    [Display(Name = "عکس پروفایل")]
    [FileImage(ErrorMessage = "تصویر پروفایل نامعتبر می باشد")]
    public IFormFile? Avatar { get; set; }



    [Display(Name = "نام")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Name { get; set; }


    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Family { get; set; }


    [Display(Name = "شماره موبایل")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "شماره تلفن نامعتبر می باشد ")]
    [MinLength(11, ErrorMessage = "شماره تلفن نامعتبر می باشد ")]
    public string PhoneNumber { get; set; }



    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Email { get; set; }



    public Gender Gender { get; set; } = Gender.None;


    #endregion
    public async Task OnGet()
    {
        var user = await userService.GetCurrentUser();
        Name = user.Name;
        Family=user.Family;
        PhoneNumber = user.PhoneNumber;
        Email=user.Email;
        Gender=user.Gender;
        
    }

    public async Task<IActionResult> OnPost()
    {
        var result = await userService.EditUserCurrent(new EditUserCommand
        {
            Name = Name,
            Family = Family,
            PhoneNumber = PhoneNumber,
            Email = Email,
            Avatar = Avatar,
            Gender = Gender
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));

    }
}

