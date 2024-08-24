using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;

namespace Eshop.RazorPage.Models.Banners;

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