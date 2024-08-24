namespace Eshop.RazorPage.Models.Banners;

public class BannerDto : BaseDto
{
    public string Link { get; set; }

    public string ImageName { get; set; }

    public BannerPositions Positions { get; set; }
}