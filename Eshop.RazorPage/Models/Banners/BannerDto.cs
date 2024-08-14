using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;

namespace Eshop.RazorPage.Models.Banners;

public class BannerDto : BaseDto
{
    public string Link { get; set; }

    public string ImageName { get; set; }

    public BannerPositions Positions { get; set; }
}

public class CreateBannerCommand
{
    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Url)]
    public string Link { get; set; }

    [Display(Name = "عکس بنر")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileImage(ErrorMessage = "عکس بنر نا معتبر می باشد")]
    public IFormFile ImageFile { get; set; }

    public BannerPositions Positions { get; set; }

}

public class EditBannerCommand
{
    public long BannerId { get; set; }


    [Display(Name = "لینک")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [DataType(DataType.Url)]
    public string Link { get; set; }


    [Display(Name = "عکس بنر")]
    [FileImage(ErrorMessage = "عکس بنر نا معتبر می باشد")]
    public IFormFile? ImageFile { get; set; }

    public BannerPositions Positions { get; set; }

}

public enum BannerPositions
{
    زیر_اسلایدر,
    سمت_راست_اسلایدر
}