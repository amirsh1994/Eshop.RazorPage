using Eshop.RazorPage.Services.Auth;
using Eshop.RazorPage.Services.Banners;
using Eshop.RazorPage.Services.Categories;
using Eshop.RazorPage.Services.Comments;
using Eshop.RazorPage.Services.Orders;
using Eshop.RazorPage.Services.Products;
using Eshop.RazorPage.Services.Roles;
using Eshop.RazorPage.Services.Sellers;
using Eshop.RazorPage.Services.Users;

namespace Eshop.RazorPage.Infrastructure;

public static class RegisterServices
{
    public static void RegisterApiServices(this IServiceCollection service)
    {
        const string baseAddress = "https://localhost:5001";
        service.AddHttpClient<IAuthService, AuthService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        });
        service.AddHttpClient<IBannerService,BannerService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        });
        service.AddHttpClient<ICategoryService, CategoryService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        });
        service.AddHttpClient<ICommentService, CommentService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        });
        service.AddHttpClient<IOrderService, OrderService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        });
        service.AddHttpClient<IProductService,ProductService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        });
        service.AddHttpClient<IRoleService,RoleService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        });
        service.AddHttpClient<ISellerService, SellerService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        });
        service.AddHttpClient<IUserService, UserService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        });
    }
}