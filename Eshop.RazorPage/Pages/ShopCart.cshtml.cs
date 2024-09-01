using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Models.Orders.Commands;
using Eshop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages;

public class ShopCartModel(IOrderService oreOrderService) : BaseRazorPage
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
            return Page();
        }
    }

    public async Task<IActionResult> OnPostAddItem(long inventoryId, int count)
    {

        if (User.Identity is{IsAuthenticated:true})
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
            return Page();
        }
    }

    public async Task<IActionResult> OnPostIncreaseItemCount(long id)
    {
        return await AjaxTryCatch(() => oreOrderService.IncreaseOrderItemCount(new IncreaseCountCommand
        {
            UserId = User.GetUserId(),
            ItemId = id,
            Count = 1
        }));
    }

    public async Task<IActionResult> OnPostDecreaseItemCount(long id)
    {
        return await AjaxTryCatch(() => oreOrderService.DecreaseOrderItemCount(new DecreaseCountCommand
        {
            UserId = User.GetUserId(),
            ItemId = id,
            Count = 1
        }));
    }
}

