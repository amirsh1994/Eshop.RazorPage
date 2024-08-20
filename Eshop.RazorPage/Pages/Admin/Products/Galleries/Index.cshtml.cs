using Eshop.RazorPage.Infrastructure.Utils.CustomValidation.IFormFile;
using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Products.Commands;

namespace Eshop.RazorPage.Pages.Admin.Products.Galleries;

[BindProperties]
public class IndexModel(IProductService productService) : BaseRazorPage
{
    public List<ProductImageDto> Images { get; set; }

    [Display(Name = "ترتیب نمایش ")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public int Sequence { get; set; }


    [Display(Name = "عکس محصول")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    [FileImage(ErrorMessage = "عکس نامعتبر می باشد")]
    public IFormFile ImageFile { get; set; }


    public async Task<IActionResult> OnGet(long productId)
    {
        var product = await productService.GetProductById(productId);
        Images = product.ImagesDtos;
        return Page();

    }

    public async Task<IActionResult> OnPost(long productId)
    {
        
        return await AjaxTryCatch(async () =>
        {
            var result = await productService.AddProductImage(new AddProductImageCommand
            {
                ImageFile = ImageFile,
                ProductId = productId,
                Sequence = Sequence
            });
            return result;
        });

    }

    public async Task<IActionResult> OnPostDeleteItem(long productId,long id)
    {
        return await AjaxTryCatch(() => productService.DeleteProductImage(new DeleteProductImageCommand()
        {
            ProductId = productId,
            ImageId = id
        }),checkModelState:false);
    }
}

