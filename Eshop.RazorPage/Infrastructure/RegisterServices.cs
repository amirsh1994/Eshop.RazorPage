using System.Linq;
using System.Net;
using Eshop.RazorPage.Infrastructure.RazorUtils;
using Eshop.RazorPage.Services.Auth;
using Eshop.RazorPage.Services.Banners;
using Eshop.RazorPage.Services.Categories;
using Eshop.RazorPage.Services.Comments;
using Eshop.RazorPage.Services.Orders;
using Eshop.RazorPage.Services.Products;
using Eshop.RazorPage.Services.Roles;
using Eshop.RazorPage.Services.Sellers;
using Eshop.RazorPage.Services.UserAddress;
using Eshop.RazorPage.Services.Users;

namespace Eshop.RazorPage.Infrastructure;

public static class RegisterServices
{
    public static void RegisterApiServices(this IServiceCollection service)
    {
        service.AddHttpContextAccessor();
        service.AddScoped<HttpClientAuthorizationDelegatingHandler>();
        service.AddScoped<IRenderViewToString, RenderViewToString>();
        service.AddAutoMapper(typeof(RegisterServices).Assembly);







        var baseAddress = "https://localhost:5001/api/";
        service.AddHttpClient<IAuthService, AuthService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>(); ;

        service.AddHttpClient<IBannerService, BannerService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();


        service.AddHttpClient<ICategoryService, CategoryService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();



        service.AddHttpClient<ICommentService, CommentService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();



        service.AddHttpClient<IOrderService, OrderService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();



        service.AddHttpClient<IProductService, ProductService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();



        service.AddHttpClient<IRoleService, RoleService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();


        service.AddHttpClient<ISellerService, SellerService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();


        service.AddHttpClient<IUserService, UserService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();



        service.AddHttpClient<IUserAddressService, UserAddressService>(op =>
        {
            op.BaseAddress = new Uri(baseAddress);
        }).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
    }
}

public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(token) == false)
            {
                request.Headers.Add("Authorization", token);
            }
        }
        return await base.SendAsync(request, cancellationToken);
    }
}