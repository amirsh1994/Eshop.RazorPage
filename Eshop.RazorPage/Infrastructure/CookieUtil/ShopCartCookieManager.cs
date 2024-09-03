using CookieManager;
using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Orders;
using Eshop.RazorPage.Services.Products;
using Eshop.RazorPage.Services.Sellers;
using StackExchange.Redis;

namespace Eshop.RazorPage.Infrastructure.CookieUtil;

public class ShopCartCookieManager(ICookieManager cookieManager, ISellerService sellerService, IProductService productService)
{
    private const string CookieShopCart = "shop-cart";

    #region GetFromCookie
    public OrderDto? Get()
    {
        return cookieManager.Get<OrderDto>(CookieShopCart);
    }
    #endregion


    #region AddToCookie
    public async Task<ApiResult> Add(long inventoryId, int count)
    {
        var shopCart = Get();
        var inventory = await sellerService.GetSellerInventoryById(inventoryId);
        if (inventory == null)
            return ApiResult.Error("inventory not found");
        var p = await productService.GetProductById(inventory.ProductId);

        if (shopCart == null)
        {
            var order = new OrderDto
            {
                Id = 1,
                CreationDate = DateTime.Now,
                UserId = 1,
                UserFullName = "",
                LastUpdate = null,
                Status = OrderStatus.Finally,
                Items =
                [
                    new OrderItemDto
                    {
                        Id = GenerateId(),
                        CreationDate = DateTime.Now,
                        OrderId = 1,
                        ProductTitle = inventory.ProductTitle,
                        ProductSlug = p.Slug,
                        ProductImageName = inventory.ProductImage,
                        ShopName = inventory.ShopName,
                        InventoryId = inventoryId,
                        Count = count,
                        Price =inventory.Price
                    }
                ],
                Discount = null,
                Methode = null,
                Address = null
            };

            SetCookie(order);
            return ApiResult.Success();
        }
        else
        {
            if (shopCart.Items.Any(x => x.InventoryId == inventoryId))//زمانی میره تو این که کانت داخل انبار موجود باشد 
            {
                var item = shopCart.Items.First(x => x.InventoryId == inventoryId);
                if (inventory.Count > item.Count + count)
                {
                    item.Count += count;
                }
                else
                {
                    return ApiResult.Error("تعداد موجودی انبار کمتر از مقدار درخواست شده می باشد..!");
                }
            }
            else
            {
                var newItem = new OrderItemDto
                {
                    Id = GenerateId(),
                    CreationDate = DateTime.Now,
                    OrderId = 1,
                    ProductTitle = inventory.ProductTitle,
                    ProductSlug = p.Slug,
                    ProductImageName = inventory.ProductImage,
                    ShopName = inventory.ShopName,
                    InventoryId = inventoryId,
                    Count = count,
                    Price = inventory.Price
                };
                shopCart.Items.Add(newItem);
            }
            SetCookie(shopCart);
            return ApiResult.Success();
        }
    }


    #endregion


    #region DeleteFromCookie

    public void Delete(long itemId)
    {
        var shopCart = Get();
        if (shopCart == null)
            return;

        var item = shopCart.Items.FirstOrDefault(x => x.Id == itemId);
        if (item == null)
            return;
        shopCart.Items.Remove(item);
        SetCookie(shopCart);
    }

    #endregion


    #region Increase

    public void Increase(long itemId)
    {

        var shopCart = Get();
        if (shopCart == null)
            return;

        var item = shopCart.Items.FirstOrDefault(x => x.Id == itemId);
        if (item == null)
            return;
        item.Count += 1;
        SetCookie(shopCart);
    }
    #endregion


    #region Decrease

    public void Decrease(long itemId)
    {

        var shopCart = Get();
        if (shopCart == null)
            return;

        var item = shopCart.Items.FirstOrDefault(x => x.Id == itemId);
        if (item == null)
            return;
        item.Count -= 1;
        SetCookie(shopCart);
    }

    #endregion


    #region HelperMethodes

    private long GenerateId()
    {
        var random = new Random();
        var number = random.Next(0, 10000) * 6 ^ 2 + random.Next(6, 1000000);
        return number;
    }

    private void SetCookie(OrderDto order)
    {
        cookieManager.Set(CookieShopCart, order, new CookieOptions()
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTimeOffset.Now.AddDays(7),
        });
    }

    #endregion


}