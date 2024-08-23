namespace Eshop.RazorPage.ViewModels.Sellers;

public class AddSellerInventoryViewModel
{
    public long SellerId { get; set; }

    public long ProductId { get; set; }

    public int Count { get; set; }

    public int Price { get; set; }

    public int? PercentageDiscount { get; set; }

}

public class EditSellerInventoryViewModel
{
    public long SellerId { get; set; }

    public long InventoryId { get; set; }

    public int Count { get; set; }

    public int Price { get; set; }

    public int? DiscountPercentage { get; set; }
}