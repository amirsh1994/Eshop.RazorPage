namespace Eshop.RazorPage.Models.Orders;

public class OrderDto : BaseDto
{
    public long UserId { get; set; }

    public string UserFullName { get; set; }

    public DateTime? LastUpdate { get; set; }

    public OrderStatus Status { get; set; }

    public List<OrderItemDto> Items { get; set; }

    public OrderDiscount? Discount { get; set; }

    public OrderShippingMethod Methode { get; set; }

    public OrderAddress? Address { get; set; }
}

public class OrderItemDto : BaseDto
{
    public long OrderId { get; set; }

    public string ProductTitle { get; set; }

    public string ProductSlug { get; set; }

    public string ProductImageName { get; set; }

    public string ShopName { get; set; }

    public long InventoryId { get; set; }

    public int Count { get; set; }

    public int Price { get; set; }

    public int TotalPrice => Price * Count;
}


public enum OrderStatus
{
    Pending,
    Finally,
    Shipping,
    Rejected
}

public class OrderDiscount 
{
    public string DiscountTitle { get; set; }

    public int DiscountAmount { get; set; }

   

    
}

public class OrderShippingMethod 
{
    public string ShippingType { get;  set; }

    public int ShippingCost { get;  set; }
}

public class OrderAddress 
{
    public long OrderId { get; set; }

    public string Shire { get; set; }

    public string City { get;  set; }

    public string PostalCode { get;  set; }

    public string PostalAddress { get; set; }

    public string PhoneNumber { get; set; }

    public string Family { get; set; }

    public string Name { get;  set; }

    public string NationalCode { get;  set; }

}

public class OrderFilterData : BaseDto
{
    public long UserId { get; set; }

    public string UserFullName { get; set; }

    public OrderStatus Status { get; set; }

    public string? Shire { get; set; }

    public string? City { get; set; }

    public int TotalPrice { get; set; }

    public int TotalItemCount { get; set; }

    public string? ShippingType { get; set; }

}

public class OrderFilterParams : BaseFilterParam
{
    public long? UserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public OrderStatus? Status { get; set; }


}

public class OrderFilterResult : BaseFilter<OrderFilterData, OrderFilterParams>
{

}