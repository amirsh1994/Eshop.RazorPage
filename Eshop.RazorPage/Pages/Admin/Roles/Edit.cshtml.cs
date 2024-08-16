using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Roles;
using Eshop.RazorPage.Services.Roles;
using Microsoft.AspNetCore.Mvc;
using Permission = Eshop.RazorPage.Models.Roles.Permission;

namespace Eshop.RazorPage.Pages.Admin.Roles;

[BindProperties]
public class EditModel(IRoleService roleService) : BaseRazorPage
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }


    public List<Permission> Permissions { get; set; }




    public async Task<IActionResult> OnGet(long id)
    {
        var role = await roleService.GetRoleById(id);
        if (role == null)
        {
            RedirectToPage("Index");
        }

        Title = role.Title;
        Permissions = role.Permissions;
        return Page();
    }

    public async Task<IActionResult> OnPost(long id, List<Permission> permissions)
    {
        var result = await roleService.EditRole(new EditRoleCommand
        {
            Id = id,
            Title = Title,
            Permissions = permissions
        });
      return  RedirectAndShowAlert(result, RedirectToPage("Index"),RedirectToPage("Edit",new{id}));
    }
}

