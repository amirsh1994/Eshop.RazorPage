using Eshop.RazorPage.Models;
using Eshop.RazorPage.Services.MainPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace Eshop.RazorPage.Pages;

[BindProperties]
public class IndexModel(IMainPageService mainPageService,IMemoryCache memoryCache) : PageModel
{
    public MainPageDto  MainPageData { get; set; }

    public async Task OnGet()
    {
        MainPageData =await memoryCache.GetOrCreateAsync("main-page",async (entry) =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(15);
            entry.SlidingExpiration = TimeSpan.FromMinutes(5);
            return await mainPageService.GetMainPageData();
        }) ?? throw new InvalidOperationException();
    }
}

