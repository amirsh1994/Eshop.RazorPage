using Eshop.RazorPage.Models;
using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Models.Products.ProductShop;
using Eshop.RazorPage.Services.Banners;
using Eshop.RazorPage.Services.Products;
using Eshop.RazorPage.Services.Sliders;

namespace Eshop.RazorPage.Services.MainPage;

public interface IMainPageService
{
    Task<MainPageDto> GetMainPageData();
}

public class MainPageService(ISliderService sliderService,IBannerService bannerService,IProductService productService):IMainPageService
{
    public async Task<MainPageDto> GetMainPageData()
    {
        var sliders = await sliderService.GetSliders();
        var banners = await bannerService.GetBannerList();
        var latestProductsResults = await productService.GetProductForShop(new ProductShopFilterParam
        {
            PageId = 1,
            Take = 8,
             SearchOrderBy = ProductSearchOrderBy.Latest

        });
        var latestProducts = latestProductsResults.Data;

        var specialProductsResults = await productService.GetProductForShop(new ProductShopFilterParam
        {
            PageId = 1,
            Take = 8,
            JustHasDiscount = true,
        });
        var specialProducts = specialProductsResults.Data;



        var topVisitProductsResults = await productService.GetProductForShop(new ProductShopFilterParam
        {
            PageId = 1,
            Take = 8,
        });
        var topVisitProducts = topVisitProductsResults.Data;

        return new MainPageDto
        {
            Sliders = sliders,
            Banners = banners,
            SpecialProducts = specialProducts,
            LatestProducts = latestProducts,
            TopVisitProducts = topVisitProducts
        };
    }
}