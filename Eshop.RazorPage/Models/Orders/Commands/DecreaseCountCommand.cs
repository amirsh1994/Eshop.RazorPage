namespace Eshop.RazorPage.Models.Orders.Commands;

public class DecreaseCountCommand
{
    public int UserId { get; set; }

    public int ItemId { get; set; }

    public int Count { get; set; }
}