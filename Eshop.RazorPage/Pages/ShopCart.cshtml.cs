using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Infrastructure.CookieUtil;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Models.Orders.Commands;
using Eshop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages;

public class ShopCartModel(IOrderService oreOrderService, ShopCartCookieManager cookieManager) : BaseRazorPage
{
    public OrderDto? OrderDto { get; set; }

    public async Task OnGet()
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            OrderDto = await oreOrderService.GetCurrentOrder();
        }
        else
        {
            OrderDto = cookieManager.Get();
        }

    }

    public async Task<IActionResult> OnPostDeleteItem(long id)
    {
        if (User.Identity is{IsAuthenticated:true})
        {

            return await AjaxTryCatch(() => oreOrderService.DeleteOrderItem(new DeleteOrderItemCommand
            {
                OrderItemId = id
            }));
        }
        else
        {
            return await AjaxTryCatch(async () =>
            {
                  cookieManager.Delete(id);
                  return ApiResult.Success();
            });
        }
    }

    public async Task<IActionResult> OnPostAddItem(long inventoryId, int count)
    {

        if (User.Identity is {IsAuthenticated:true})
        {
            return await AjaxTryCatch(() => oreOrderService.AddOrderItem(new AddOrderItemCommand
            {
                InventoryId = inventoryId,
                Count = count,
                UserId = User.GetUserId()
            }));
        }
        else
        {
            return await AjaxTryCatch(() => cookieManager.Add(inventoryId, count));
        }
    }

    public async Task<IActionResult> OnPostIncreaseItemCount(long id)
    {
        if (User.Identity is {IsAuthenticated:true})
        {
            return await AjaxTryCatch(() => oreOrderService.IncreaseOrderItemCount(new IncreaseCountCommand
            {
                UserId = User.GetUserId(),
                ItemId = id,
                Count = 1
            }));
        }
        else
        {
            return await AjaxTryCatch(async () =>
            {
                cookieManager.Increase(id);
                return ApiResult.Success();
            });
        }
    }

    public async Task<IActionResult> OnPostDecreaseItemCount(long id)
    {
        if (User.Identity is {IsAuthenticated:true})
        {
            return await AjaxTryCatch(() => oreOrderService.DecreaseOrderItemCount(new DecreaseCountCommand
            {
                UserId = User.GetUserId(),
                ItemId = id,
                Count = 1
            }));
        }
        else
        {
            return await AjaxTryCatch(async () =>
            {
                 cookieManager.Decrease(id);
                 return ApiResult.Success();
            });
        }
    }

    public async Task<IActionResult> OnGetShopCartDetail()
    {
        var order = new OrderDto();
        if (User.Identity is { IsAuthenticated: true })
        {
             order = await oreOrderService.GetCurrentOrder();
        }
        else
        {
             order = cookieManager.Get();
        }

        return new ObjectResult(new
        {
            items=order?.Items,
            count=order?.Items.Sum(x=>x.Count),
            price=$"{order?.Items.Sum(x => x.TotalPrice):#,0}تومان"
        });
    }
}

