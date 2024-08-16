using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Infrastructure.Utils;
using Eshop.RazorPage.Models.Roles;
using Eshop.RazorPage.Services.Roles;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Admin.Roles;

[BindProperties]
public class AddModel(IRoleService roleService) : BaseRazorPage
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }

/*
    public List<Permission> RolePermission { get; set; }
*/

    
    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPost(string[] Permission)
    {
        var permissionModel = new List<Permission>();
        foreach (var item in Permission)
        {
            try
            {
                permissionModel.Add(EnumUtil.ParseEnum<Permission>(item));
            }
            catch 
            {
                //
            }
        }
        var result = await roleService.CreateRole(new CreateRoleRoleCommand()
        {
            Title = Title,
            Permissions = permissionModel
        });

        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

