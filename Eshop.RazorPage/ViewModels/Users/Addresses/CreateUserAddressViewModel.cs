using System.ComponentModel.DataAnnotations;

namespace Eshop.RazorPage.ViewModels.Users.Addresses;

public class CreateUserAddressViewModel
{
    [Display(Name = "نام استان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Shire { get; set; }

    [Display(Name = "نام شهر")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string City { get; set; }


    [Display(Name = "کد پستی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PostalCode { get; set; }



    [Display(Name = "ادرس پستی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PostalAddress { get; set; }



    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "شماره تلفن نامتعتبر می باشد")]
    [MinLength(11, ErrorMessage = "شماره تلفن نامتعتبر می باشد")]
    public string PhoneNumber { get; set; }



    [Display(Name = "نام")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Name { get; set; }



    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Family { get; set; }


    [Display(Name = "کدملی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MaxLength(10, ErrorMessage = "کد ملی نامعتبر میباشد")]
    [MinLength(10, ErrorMessage = "کد ملی نامعتبر میباشد")]
    public string NationalCode { get; set; }
}

public class EditUserAddressViewModel
{
    public long Id { get; set; }

    [Display(Name = "نام استان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Shire { get; set; }

    [Display(Name = "نام شهر")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string City { get; set; }


    [Display(Name = "کد پستی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PostalCode { get; set; }



    [Display(Name = "ادرس پستی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string PostalAddress { get; set; }



    [Display(Name = "شماره تلفن")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "شماره تلفن نامتعتبر می باشد")]
    [MinLength(11, ErrorMessage = "شماره تلفن نامتعتبر می باشد")]
    public string PhoneNumber { get; set; }



    [Display(Name = "نام")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Name { get; set; }



    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Family { get; set; }


    [Display(Name = "کدملی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [MaxLength(10, ErrorMessage = "کد ملی نامعتبر میباشد")]
    [MinLength(10, ErrorMessage = "کد ملی نامعتبر میباشد")]
    public string NationalCode { get; set; }
}