using Eshop.RazorPage.Models.Sellers;
using Eshop.RazorPage.Services.Sellers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.SellerPanel.Inventories;

public class IndexModel(ISellerService sellerService) : PageModel
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
}

