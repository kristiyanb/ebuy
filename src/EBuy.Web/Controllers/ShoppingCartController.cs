namespace EBuy.Web.Controllers
{
    using EBuy.Models;
    using EBuy.Services.Contracts;
    using EBuy.Web.Models.ShoppingCart;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await this.shoppingCartService
                .GetShoppingCartProductsByUsername<ShoppingCartProductViewModel>(this.User.Identity.Name);

            return View(new ShoppingCartViewModel { Products = products.ToList() });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ShoppingCartProductInputModel input)
        {
            await this.shoppingCartService.AddProduct(User.Identity.Name, input.Id, input.Quantity);

            return Redirect("/Products/Details/" + input.Id);
        }

        public async Task<IActionResult> Remove(string id)
        {
            await this.shoppingCartService.RemoveProduct(id);

            return Redirect("/ShoppingCart/Index");
        }
    }
}
