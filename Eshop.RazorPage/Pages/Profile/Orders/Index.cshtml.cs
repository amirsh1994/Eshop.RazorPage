using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Profile.Orders;

    public class IndexModel(IOrderService orderService) : PageModel
    {
        public OrderFilterResult FilterResult { get; set; }

        public async Task OnGet(int take=10,int pageId=1,OrderStatus ? status=null)
        {
            FilterResult = await orderService.GetUserOrdersByFilter(10, pageId, status);
        }
    }

