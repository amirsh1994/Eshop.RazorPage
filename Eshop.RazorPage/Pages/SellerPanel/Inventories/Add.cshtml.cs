using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Sellers.Commands;
using Eshop.RazorPage.Services.Sellers;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.SellerPanel.Inventories;

[BindProperties]
public class AddModel(ISellerService sellerService) : BaseRazorPage
{

    public long ProductId { get; set; }

    [Display(Name = "تعداد موجود")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Count { get; set; }


    [Display(Name = "قیمت")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Price { get; set; }


    [Display(Name = "درصد تخفیف")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Range(0,100,ErrorMessage = "درصد تخفیف نامعتبر می باشد")]
    public int PercentageDiscount { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var seller = await sellerService.GetCurrent();
        if (seller == null)
        {
            return Redirect("/");
        }
        var result = await sellerService.AddSellerInventory(new AddSellerInventoryCommand
        {
            SellerId = seller.Id,
            ProductId = ProductId,
            Count = Count,
            Price = Price,
            PercentageDiscount = PercentageDiscount
        });
        return RedirectAndShowAlert(result, RedirectToPage("Index"));
    }
}

