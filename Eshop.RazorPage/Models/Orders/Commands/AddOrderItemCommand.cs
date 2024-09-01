namespace Eshop.RazorPage.Models.Orders.Commands;

public class AddOrderItemCommand
{
    public long InventoryId { get; set; }

    public int Count { get; set; }

    public long UserId { get; set; }
}

public class DeleteOrderItemCommand
{
    public long OrderItemId { get; set; }
}