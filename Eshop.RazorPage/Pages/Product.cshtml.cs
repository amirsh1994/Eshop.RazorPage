using Eshop.RazorPage.Infrastructure;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Models.Comments;
using Eshop.RazorPage.Models.Products;
using Eshop.RazorPage.Pages.Shared.Products;
using Eshop.RazorPage.Services.Comments;
using Eshop.RazorPage.Services.Products;
using Eshop.RazorPage.Services.Sellers;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.RazorPage.Pages;

public class ProductModel(IProductService productService, ISellerService sellerService,ICommentService commentService) : BaseRazorPage
{
    public SingleProductDto productModel { get; set; }

    public async Task<IActionResult> OnGet(string slug)
    {
        var product = await productService.GetSingleProduct(slug);
        if (product == null)
            return NotFound();

        productModel = product;

        return Page();
    }

    public async Task<IActionResult> OnPost(string slug, long productId, string comment)
    {
        if (User.Identity.IsAuthenticated == false)
            return Page();

        var result = await commentService.AddComment(new CreateCommentCommand()
        {
            ProductId = productId,
            Text = comment,
            UserId = User.GetUserId()
        });
        if (result.IsSuccess == false)
        {
            ErrorAlert(result.MetaData.Message);
            return Page();
        }
        SuccessAlert("نظر شما ثبت شد ، بعد از تایید در سایت نمایش داده می شود");
        return RedirectToPage("Product", new { slug });
    }

    public async Task<IActionResult> OnGetProductComment(long productId,int pageId=1)
    {
        var commentResult = await commentService.GetProductComments(pageId,2,productId);
        return Partial("Shared/Products/_Comments",commentResult);
    }

    public async Task<IActionResult> OnPostDeleteComment(long commentId)
    {
        return await AjaxTryCatch( async () =>await commentService.DeleteComment(commentId) );

    }
}

