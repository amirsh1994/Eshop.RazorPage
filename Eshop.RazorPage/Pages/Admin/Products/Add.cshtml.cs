using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Eshop.RazorPage.Models.Products.Commands;
using Eshop.RazorPage.Services.Categories;
using Eshop.RazorPage.Services.Products;
using Eshop.RazorPage.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Admin.Products;

[BindProperties]
public class AddModel(IProductService productService, ICategoryService categoryService) : BaseRazorPage
{
    #region AddProductModel
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
    [Range(1, int.MaxValue, ErrorMessage = "دسته بندی را وارد کنید")]
    public long CategoryId { get; set; }


    [Display(Name = " زیر دسته بندی ")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [Range(1, int.MaxValue, ErrorMessage = " زیر دسته بندی را وارد کنید ")]
    public long SubCategoryId { get; set; }


    [Display(Name = "دسته بندی سوم")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public long? FirstSubCategoryId { get; set; }//Level 3 


    [Display(Name = "slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; }



    public SeoDataViewModel SeoData { get; set; }


    public List<string> Keys { get; set; } = [];


    public List<string> Values { get; set; } = [];


    #endregion

    public void OnGet()
    {
    }


    public async Task<IActionResult> OnPost()
    {
        if (FirstSubCategoryId == 0)
        {
            FirstSubCategoryId = null;
        }

        var result = await productService.CreateProduct(new CreateProductCommand
        {
            Title = Title,
            ImageFile = ImageFile,
            Description = Description,
            CategoryId = CategoryId,
            SubCategoryId = SubCategoryId,
            FirstSubCategoryId = FirstSubCategoryId,
            Slug = Slug,
            SeoData = SeoData.MapToSeoData(),
            Specifications = ConvertSpecification()
        });
        return RedirectAndShowAlert(result,RedirectToPage("Index"));
    }

    private Dictionary<string, string> ConvertSpecification()
    {
        var specification = new Dictionary<string, string>();
        Keys.RemoveAll(x => x == null || string.IsNullOrWhiteSpace(x));
        Values.RemoveAll(f => f == null || string.IsNullOrWhiteSpace(f));
        for (var i = 0; i < Keys.Count; i++)
        {
            specification.Add(Keys[i], Values[i]);
        }

        return specification;
    }
}


