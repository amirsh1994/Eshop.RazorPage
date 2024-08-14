namespace Eshop.RazorPage.Infrastructure;

public class Directories
{
    public const string ProductImages = "/images/Products";

    public const string ProductGalleyImages = "/images/Products/gallery";

    public const string BannerImages = "/images/banners";

    public const string SliderImages = "/images/sliders";

    public const string UserAvatar = "/images/users/avatar";


    public static string GetSlider(string imageName)
    {
        return $"{SiteSettings.ServerPath}{SliderImages}/{imageName}";
    }

    public static string GetBannerImage(string imageName)
    {
        return $"{SiteSettings.ServerPath}{BannerImages}/{imageName}";
    }

}