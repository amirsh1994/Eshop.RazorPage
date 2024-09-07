using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Checkout;

    public class FinallyOrderModel(IOrderService orderService) : PageModel
    {
        public OrderDto?  Order { get; set; }

        public async Task<IActionResult> OnGet(long orderId)
        {
            Order = await orderService.GetOrderById(orderId);
            if (Order == null || Order.UserId != User.GetUserId())
            {
                return Redirect("/");
            }
            return Page();
        }
    }

