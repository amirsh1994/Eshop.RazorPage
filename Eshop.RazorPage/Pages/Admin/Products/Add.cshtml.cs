using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Eshop.RazorPage.Services.Categories;
using Eshop.RazorPage.Services.Products;
using Eshop.RazorPage.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages.Admin.Products;

[BindProperties]
public class AddModel(IProductService productService, ICategoryService categoryService) : BaseRazorPage
{
    [Display(Name = "عنوان محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }


    [Display(Name = "عکس محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileImage(ErrorMessage = "عکس نامعتبر می باشد")]
    public IFormFile ImageFile { get; set; }



    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [UIHint("Ckeditor4")]
    public string Description { get; set; }


    [Display(Name = "دسته بندی اصلی")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public long CategoryId { get; set; }


    [Display(Name = " زیر دسته بندی ")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public long SubCategoryId { get; set; }


    [Display(Name = "دسته بندی سوم")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public long? FirstSubCategoryId { get; set; }//Level 3 


    [Display(Name = "slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }



    public SeoDataViewModel SeoData { get; set; }

    public Dictionary<string, string> Specifications { get; set; }

    public void OnGet()
    {
    }


}

