namespace EBuy.Web.Controllers
{
    using EBuy.Models;
    using EBuy.Services.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CheckoutController : Controller
    {
        private readonly ICheckoutService checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            this.checkoutService = checkoutService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
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
