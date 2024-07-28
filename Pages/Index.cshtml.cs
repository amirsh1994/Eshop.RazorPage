using Eshop.RazorPage.Models.Auth;
using Eshop.RazorPage.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Eshop.RazorPage.Pages;

    public class IndexModel(ILogger<IndexModel> logger,IAuthService authService) : PageModel
    {
        private readonly ILogger<IndexModel> _logger = logger;

        public async Task OnGet()
        {


        }
    }

