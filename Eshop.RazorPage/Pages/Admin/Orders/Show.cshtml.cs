using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Orders;

public class ShowModel(IOrderService orderService):BaseRazorPage
{
    public OrderDto Order { get; set; }

    public async Task<IActionResult> OnGet(long id)
    {
        var order = await orderService.GetOrderById(id);
        if (order == null)
        {
            return RedirectToPage("Index");
        }
        Order = order;
        return Page();
    }

    public async Task<IActionResult> OnPost(long id)
    {
        var result = await orderService.SendOrder(id);
        return RedirectAndShowAlert(result,RedirectToPage("Show", new{id}));
    }
}

