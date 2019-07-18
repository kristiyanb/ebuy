namespace EBuy.Web.Controllers
{
    using EBuy.Services.Contracts;
    using EBuy.Web.Models.ShoppingCart;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
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
            await this.checkoutService.Checkout(this.User.Identity.Name, address);

            return Redirect("/Checkout/SuccessfulOrder");
        }

        public async Task<IActionResult> SuccessfulOrder()
        {
            return View();
        }
    }
}
