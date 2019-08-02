namespace EBuy.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
    using Models.Users;

    public class CheckoutController : Controller
    {
        private readonly IUserService userService;

        public CheckoutController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> ShoppingCart()
        {
            return this.View();
        }

        public async Task<IActionResult> Order()
        {
            var user = await this.userService.GetUserByUserName<UserViewModel>(this.User.Identity.Name);

            return this.View(user);
        }
    }
}
