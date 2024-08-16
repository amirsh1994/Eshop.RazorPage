using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Roles;
using Eshop.RazorPage.Services.Roles;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Admin.Roles;

[BindProperties]
public class IndexModel(IRoleService roleService) : BaseRazorPage
{
    public List<RoleDto> Roles { get; set; }

    public async Task OnGet()
    {
        Roles = await roleService.GetRoles();
    }

    
}

