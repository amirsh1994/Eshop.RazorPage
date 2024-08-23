using System.ComponentModel;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Sellers;
using Eshop.RazorPage.Models.Sellers.Commands;
using Eshop.RazorPage.Services.Sellers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.SellerPanel.Inventories;

[BindProperties]
public class IndexModel(ISellerService sellerService,IRenderViewToString renderViewToString) : BaseRazorPage
{
    public List<InventoryDto> Inventories { get; set; }

    public async Task<IActionResult> OnGet()
    {
        var seller = await sellerService.GetCurrent();

        if (seller == null)
            Redirect("/");

        Inventories = await sellerService.GetInventories();

        return Page();
    }

    public async Task<IActionResult> OnGetEditPage(long inventoryId)
    {
        return await AjaxTryCatch(async () =>
        {
            var inventory = await sellerService.GetSellerInventoryById(inventoryId);
            if(inventory==null)
                return ApiResult<string>.Success("اطلاعات نامعتبر می باشد");
            var view = await renderViewToString.RenderToStringAsync("_Edit", new EditSellerInventoryCommand
            {
                SellerId = inventory.SellerId,
                InventoryId = inventory.Id,
                Count = inventory.Count,
                Price = inventory.Price,
                DiscountPercentage = inventory.DiscountPercentage
            }, PageContext);
            return ApiResult<string>.Success(view);
        });

    }

    public async Task<IActionResult> OnPost(EditSellerInventoryCommand command)
    {
        return await AjaxTryCatch(async () => await sellerService.EditSellerInventory(command));
    }

}

