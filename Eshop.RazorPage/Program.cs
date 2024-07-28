using Eshop.RazorPage.Infrastructure;

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

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}

