namespace Eshop.RazorPage.Models.Banners;

public class BannerDto:BaseDto
{
    public string Link { get; set; }

    public string ImageName { get; set; }

    public BannerPositions Positions { get; set; }
}

public class CreateBannerCommand
{
    public string Link { get; set; }

    public IFormFile ImageFile { get; set; }

    public BannerPositions Positions { get; set; }

}

public class EditBannerCommand
{
    public long BannerId { get; set; }

    public string Link { get; set; }

    public IFormFile ImageFile { get; set; }

    public BannerPositions Positions { get; set; }

}

public enum BannerPositions
{
    زیر_اسلایدر,
    سمت_راست_اسلایدر
}