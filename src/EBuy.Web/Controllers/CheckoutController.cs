namespace EBuy.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    using EBuy.Common;
    using EBuy.Models;
    using EBuy.Services.Contracts;
    using Models.Users;

    public class CheckoutController : Controller
    {
        private readonly ICheckoutService checkoutService;
        private readonly IUserService userService;

        public CheckoutController(ICheckoutService checkoutService, IUserService userService)
        {
            this.checkoutService = checkoutService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userService.GetUserByUserName<UserViewModel>(this.User.Identity.Name);

            return this.View(user);
        }

        public async Task<IActionResult> Order(string address)
        {
            if (this.User.Identity.Name == null)
            {
                var cart = this.HttpContext.Session.GetString(GlobalConstants.GuestCartKey);

                await this.checkoutService.CheckoutAsGuest(cart, address);
                
                this.HttpContext.Session
                    .SetString(GlobalConstants.GuestCartKey, JsonConvert.SerializeObject(new List<GuestCartProduct>()));
            }
            else
            {
                await this.checkoutService.Checkout(this.User.Identity.Name, address);
            }

            return this.Redirect("/Checkout/SuccessfulOrder");
        }

        public async Task<IActionResult> SuccessfulOrder()
        {
            return this.View();
        }
    }
}
