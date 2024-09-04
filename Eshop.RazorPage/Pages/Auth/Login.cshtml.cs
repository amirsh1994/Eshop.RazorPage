using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Infrastructure.CookieUtil;
using Eshop.RazorPage.Models.Auth;
using Eshop.RazorPage.Models.Orders.Commands;
using Eshop.RazorPage.Services.Auth;
using Eshop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Auth;
[BindProperties]
[ValidateAntiForgeryToken]
public class LoginModel(IAuthService authService, ShopCartCookieManager cookieManager, IOrderService orderService) : PageModel
{
    #region Models


    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PhoneNumber { get; set; }



    [Display(Name = "رمز عبور")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Password)]
    public string Password { get; set; }


    public string RedirectTo { get; set; }

    #endregion




    #region Get

    public IActionResult OnGet(string redirectTo)
    {
        if (User.Identity.IsAuthenticated)
        {
            return Redirect("/");
        }

        RedirectTo = redirectTo;
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
            ModelState.AddModelError(nameof(PhoneNumber), result.MetaData.Message);
            return Page();
        }

        var token = result?.Data.Token;
        var refreshToken = result?.Data.RefreshToken;
        if (token != null) HttpContext.Response.Cookies.Append("token", token, new CookieOptions()
        {
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddDays(7)
        });
        if (refreshToken != null) HttpContext.Response.Cookies.Append("refresh-token", refreshToken, new CookieOptions()
        {
            HttpOnly = true
           ,
            Expires = DateTimeOffset.Now.AddDays(10)
        });
       
        await SyncShopCart(token);
        if (string.IsNullOrWhiteSpace(RedirectTo) == false)
        {
            return LocalRedirect(RedirectTo);
        }

        return Redirect("/");

    }

    private async Task SyncShopCart(string token)
    {
        var shopCart = cookieManager.Get();
        if (shopCart == null || shopCart.Items.Any() == false)
            return;
        HttpContext.Request.Headers.Append("Authorization", $"Bearer {token}");//چون دفعه اول داریم میریم سمت سرور توکن نداریم که بریم ایتم های اردر به سمت سرور هم اد بشن
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var userId = Convert.ToInt64(jwtSecurityToken.Claims.First(claim=>claim.Type==ClaimTypes.NameIdentifier).Value);

        foreach (var item in shopCart.Items)
        {
            await orderService.AddOrderItem(new AddOrderItemCommand
            {
                InventoryId = item.InventoryId,
                Count = item.Count,
                UserId = userId
            });
        }
        cookieManager.DeleteShopCart();
    }
    #endregion




}

