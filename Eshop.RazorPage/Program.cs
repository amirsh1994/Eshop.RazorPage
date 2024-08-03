using Eshop.RazorPage.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Eshop.RazorPage;

public class Program
{
    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
        builder.Services.RegisterApiServices();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            option.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudience = builder.Configuration["JwtConfig:Audience"],
                ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SignInKey"])),
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true
            };
        });
        //builder.Services.AddScoped(typeof(CancellationToken), serviceProvider =>
        //{
        //    IHttpContextAccessor httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        //    return httpContext.HttpContext?.RequestAborted ?? CancellationToken.None;
        //});

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.Use(async (context, next) =>
        {
            var token = context.Request.Cookies["token"]?.ToString();
            if (string.IsNullOrWhiteSpace(token) == false)
            {
                context.Request.Headers.Append("Authorization", $"Bearer {token}");
            }
            await next();
        });
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}

