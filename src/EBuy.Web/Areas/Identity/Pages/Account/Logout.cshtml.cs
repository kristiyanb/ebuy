namespace EBuy.Web.Areas.Identity.Pages.Account
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    using EBuy.Services.Contracts;
    using EBuy.Models;

    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IUserService userService;

        public LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger, IUserService userService)
        {
            _signInManager = signInManager;
            _logger = logger;
            this.userService = userService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await this.userService.SetLastOnlineNow(this.User.Identity.Name);
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }

            return Page();
        }
    }
}