using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Models.Orders.Commands;
using Eshop.RazorPage.Models.ShippingMethods;
using Eshop.RazorPage.Models.UserAddress;
using Eshop.RazorPage.Services.Orders;
using Eshop.RazorPage.Services.ShippingMethods;
using Eshop.RazorPage.Services.Transactions;
using Eshop.RazorPage.Services.UserAddress;
using Eshop.RazorPage.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Checkout;

public class IndexModel(IOrderService orderService, IUserAddressService userAddressService,IShippingMethodeService shippingService,ITransactionService transactionService) : BaseRazorPage
{
    public OrderDto Order { get; set; }

    public List<AddressDto> Addresses { get; set; } = [];

    public List<ShippingMethodDto> ShippingMethods { get; set; } = [];

    public async Task<IActionResult> OnGet()
    {
        var order = await orderService.GetCurrentOrder();
        if (order == null)
            return RedirectToPage("../Index");
        Order = order;

        Addresses = await userAddressService.GetUserAddresses();

        ShippingMethods=await shippingService.GetShippingMethods();
        if (ShippingMethods.Any()==false)
        {
            return RedirectToPage("../Index");
        }
        return Page();

    }

    public async Task<IActionResult> OnPost(long shippingMethodeId)
    {
        var address = await userAddressService.GetUserAddresses();
        var firstActiveAddress = address.FirstOrDefault(x=>x.ActiveAddress);
        if (firstActiveAddress == null)
        {
            return RedirectToPage("Index");
        }
        var result = await orderService.CheckOutOrder(new CheckOutOrderCommand
        {
            UserId = User.GetUserId(),
            Shire = firstActiveAddress.Shire,
            City = firstActiveAddress.City,
            ShippingMethodeId = shippingMethodeId,
            PostalAddress = firstActiveAddress.PostalAddress,
            PhoneNumber = firstActiveAddress.PhoneNumber,
            Family = firstActiveAddress.Family,
            Name = firstActiveAddress.Name,
            NationalCode = firstActiveAddress.NationalCode,
            PostalCode = firstActiveAddress.PostalCode,
        });
        if (result!.IsSuccess)
        {
            var currentOrder = await orderService.GetCurrentOrder();
            var transaction = await transactionService.CreateTransaction(new CreateTransactionCommand
            {
                OrderId = currentOrder!.Id,
                SuccessCallBackUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Checkout/FinallyOrder/{currentOrder?.Id}",
                ErrorCallBackUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Checkout/FinallyOrder/{currentOrder?.Id}"
            });
            if (transaction!.IsSuccess)
            {
                return Redirect(transaction.Data);
            }
        }
        ErrorAlert(result.MetaData.Message);
        return Redirect("Index");
    }
}

