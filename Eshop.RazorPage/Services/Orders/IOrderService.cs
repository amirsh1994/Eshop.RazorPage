using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Models.Orders.Commands;

namespace Eshop.RazorPage.Services.Orders;

public interface IOrderService
{
    Task<ApiResult> AddOrderItem(AddOrderItemCommand command);

    Task<ApiResult?> CheckOutOrder(CheckOutOrderCommand command);

    Task<ApiResult> IncreaseOrderItemCount(IncreaseCountCommand command);

    Task<ApiResult> DecreaseOrderItemCount(DecreaseCountCommand command);

    Task<ApiResult> DeleteOrderItem(DeleteOrderItemCommand command);

    Task<OrderFilterResult> GetOrdersByFilter(OrderFilterParams filterParams);

    Task<OrderDto> GetOrderById(long orderId);

    Task<OrderDto> GetCurrentOrder();
}

public class OrderService(HttpClient client) : IOrderService
{
    public async Task<ApiResult?> AddOrderItem(AddOrderItemCommand command)
    {
        var result = await client.PostAsJsonAsync("order", command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> CheckOutOrder(CheckOutOrderCommand command)
    {
        var result = await client.PostAsJsonAsync("order/checkOut", command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> IncreaseOrderItemCount(IncreaseCountCommand command)
    {
        var result = await client.PutAsJsonAsync("order/orderItem/IncreaseCount", command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> DecreaseOrderItemCount(DecreaseCountCommand command)
    {
        var result = await client.PutAsJsonAsync("order/orderItem/DecreaseCount", command);
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }

    public async Task<ApiResult?> DeleteOrderItem(DeleteOrderItemCommand command)
    {
        var result = await client.DeleteAsync($"order/orderItem/{command.OrderItemId}");
        var response = await result.Content.ReadFromJsonAsync<ApiResult>();
        return response;
    }


    public async Task<OrderFilterResult> GetOrdersByFilter(OrderFilterParams filterParams)
    {

        var response = await client.GetFromJsonAsync<ApiResult<OrderFilterResult>>($"order/");
        return response.Data;
    }

    public async Task<OrderDto?> GetOrderById(long orderId)
    {
        var response = await client.GetFromJsonAsync<ApiResult<OrderDto?>>($"order/{orderId}");
        return response?.Data;
    }

    public async Task<OrderDto?> GetCurrentOrder()
    {
        var result = await client.GetFromJsonAsync<ApiResult<OrderDto?>>("order/current");
        return result?.Data;
    }
}