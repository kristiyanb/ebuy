using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EBuy.Models;
using EBuy.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EBuy.Web.Areas.Identity.Pages.Account
{
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
            await this.userService.SetLastOnlineNow(User.Identity.Name);
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Page();
            }
        }
    }
}