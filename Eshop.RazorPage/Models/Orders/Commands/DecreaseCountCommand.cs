namespace Eshop.RazorPage.Models.Orders.Commands;

public class DecreaseCountCommand
{
    public long UserId { get; set; }

    public long ItemId { get; set; }

    public int Count { get; set; }
}