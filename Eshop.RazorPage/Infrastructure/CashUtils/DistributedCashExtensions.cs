using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Eshop.RazorPage.Infrastructure.CashUtils;

public static class DistributedCashExtensions
{
    public static async Task<T?> GetOrSet<T>(this IDistributedCache cash, string key, Func<Task<T>> func, CashOptions options)
    {
        var value = await cash.GetAsync(key);
        if (value == null)
        {
            var result = await func();
            if (result == null)
                return default;
            await SetCash(cash, key, result, options);
        }

        var data = JsonSerializer.Deserialize<T>(value);
        return data;
    }


    public static async Task<T?> GetOrSet<T>(this IDistributedCache cash, string key, Func<Task<T>> func)
    {
        var val = await cash.GetAsync(key);
        if (val == null)
        {
            var res = await func();
            if (res == null)
                return default;

            await SetCash(cash, key, res);
            return res;
        }
        var data = JsonSerializer.Deserialize<T>(val);
        return data;
    }


    public static Task SetCash<T>(this IDistributedCache cache, string key, T value)
    {
        return SetCash(cache, key, value, new CashOptions());
    }


    private static async Task SetCash<T>(this IDistributedCache cache, string key, T value, CashOptions options)
    {
        var json = JsonSerializer.Serialize(value);
        var bytes = Encoding.UTF8.GetBytes(json);

        await cache.SetAsync(key, bytes, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(options.AbsoluteExpirationCacheFromMinutes),
            SlidingExpiration = TimeSpan.FromMinutes(options.ExpireSlidingCacheFromMinutes),
        });
    }


    public static async Task<T?> GetAsync<T>(this IDistributedCache cash, string key)
    {
        var val = await cash.GetAsync(key);
        if (val == null)
            return default;

        var value = JsonSerializer.Deserialize<T>(val);
        return value;
    }



}

public class CashOptions
{
    public int ExpireSlidingCacheFromMinutes { get; set; } = 5;

    public int AbsoluteExpirationCacheFromMinutes { get; set; } = 10;

}

public class CashKeys
{
    public const string HomePage = "home-page";
}