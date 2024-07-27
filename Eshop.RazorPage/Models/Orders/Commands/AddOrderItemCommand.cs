namespace Eshop.RazorPage.Models.Orders.Commands;

public class AddOrderItemCommand
{
    public int InventoryId { get; set; }

    public int Count { get; set; }

    public int UserId { get; set; }
}

public class DeleteOrderItemCommand
{
    public long OrderItemId { get; set; }
}