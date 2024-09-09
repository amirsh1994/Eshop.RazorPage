using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Infrastructure.Utils;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Orders;

    public class IndexModel(IOrderService orderService) : BaseRazorFilter<OrderFilterParams>
    {
        public OrderFilterResult FilterResult { get; set; }


        public async Task OnGet(string ? startDate,string ? endDate)
        
        {
            if (string.IsNullOrWhiteSpace(startDate)==false)
            {
                FilterParam.StartDate = startDate.ToMiladi();
            }

            if (string.IsNullOrWhiteSpace(endDate)==false)
            {
                FilterParam.EndDate=endDate.ToMiladi();

            }

            FilterResult = await orderService.GetOrdersByFilter(FilterParam);
        }
    }

