using Eshop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Eshop.RazorPage.ViewModels;
using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Models.Products.Commands;
using Eshop.RazorPage.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages.Admin.Products;

[BindProperties]
public class EditModel(IProductService productService) : BaseRazorPage
{
    #region AddProductModel
    [Display(Name = "عنوان محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; }


    [Display(Name = "عکس محصول")]
    [FileImage(ErrorMessage = "عکس نامعتبر می باشد")]
    public IFormFile? ImageFile { get; set; }



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

    public async Task<IActionResult> OnGet(long productId)
    {
        var product = await productService.GetProductById(productId);
        if (product == null)
            return RedirectToPage("Index");
        CategoryId = product.Category.Id;
        Title = product.Title;
        Description = product.Description;
        SubCategoryId = product.SubCategory.Id;
        FirstSubCategoryId = product.SecondarySubCategory.Id;
        Slug = product.Slug;
        SeoData = SeoDataViewModel.MapSeoDataToViewModel(product.SeoData);
        InitSpecification(product.SpecificationsDtos);
        return Page();
    }

    public async Task<IActionResult> OnPost(long productId)
    {
        var result = await productService.EditProduct(new EditProductCommand
        {
            Id = productId,
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
        return RedirectAndShowAlert(result, RedirectToPage("Index"),RedirectToPage("Index",new{productId}));
    }


    public void InitSpecification(List<ProductSpecificationDto> specifications)
    {
        foreach (var item in specifications)
        {
            Keys.Add(item.Key);
            Values.Add(item.Value);
        }
    }

    private Dictionary<string, string> ConvertSpecification()
    {
        var specification = new Dictionary<string, string>();
        Keys.RemoveAll(x => x == null || string.IsNullOrWhiteSpace(x));
        Values.RemoveAll(x => x == null || string.IsNullOrWhiteSpace(x));
        for (int i = 0; i < Keys.Count; i++)
        {
            specification.Add(Keys[i], Values[i]);
        }

        return specification;
    }
}

