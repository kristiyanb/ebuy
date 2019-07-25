namespace EBuy.Web.Controllers
{
    using AutoMapper;
    using EBuy.Models;
    using EBuy.Services.Contracts;
    using EBuy.Web.Models.Users;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CheckoutController : Controller
    {
        private readonly ICheckoutService checkoutService;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public CheckoutController(ICheckoutService checkoutService, IUserService userService, IMapper mapper)
        {
            this.checkoutService = checkoutService;
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userService.GetUserByUserName<UserViewModel>(this.User.Identity.Name);

            return View(user);
        }

        public async Task<IActionResult> Order(string address)
        {
            if (this.User.Identity.Name == null)
            {
                var cart = HttpContext.Session.GetString("cart");

                await this.checkoutService.CheckoutAsGuest(cart, address);

                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(new List<GuestCartProduct>()));
            }
            else
            {
                await this.checkoutService.Checkout(this.User.Identity.Name, address);
            }

            return Redirect("/Checkout/SuccessfulOrder");
        }

        public async Task<IActionResult> SuccessfulOrder()
        {
            return View();
        }
    }
}
