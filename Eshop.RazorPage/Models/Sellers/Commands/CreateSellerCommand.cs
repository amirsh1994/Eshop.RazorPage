using System.ComponentModel.DataAnnotations;

namespace Eshop.RazorPage.Models.Sellers.Commands;

public class CreateSellerCommand
{
    public long UserId { get; set; }

    public string ShopName { get; set; }

    public string NationalCode { get; set; }
}

public class EditSellerCommand
{
    public long Id { get; set; }

    public string ShopName { get; set; }

    public string NationalCode { get; set; }

    public SellerStatus Status { get; set; }
}

public class AddSellerInventoryCommand
{
    public long SellerId { get; set; }

    public long ProductId { get; set; }

    public int Count { get; set; }

    public int Price { get; set; }

    public int? PercentageDiscount { get; set; }

}

public class EditSellerInventoryCommand
{
    public long SellerId { get; set; }

    public long InventoryId { get; set; }

    [Display(Name = "تعداد محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Count { get; set; }

    [Display(Name = "قیمت محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Price { get; set; }

    [Display(Name = "درصد تخفیف")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Range(0,50,ErrorMessage = "درصد تخفیف نا معتبر می باشد")]
    public int DiscountPercentage { get; set; }
}


