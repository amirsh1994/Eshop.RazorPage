using Eshop.RazorPage.Models.Banners;
using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Models.Products.ProductShop;
using Eshop.RazorPage.Models.Sliders;

namespace Eshop.RazorPage.Models;

public class MainPageDto
{
    public List<SliderDto> Sliders { get; set; }

    public List<BannerDto> Banners { get; set; }

    public List<ProductShopDto> SpecialProducts { get; set; }

    public List<ProductShopDto> LatestProducts { get; set; }

    public List<ProductShopDto> TopVisitProducts { get; set; }

}