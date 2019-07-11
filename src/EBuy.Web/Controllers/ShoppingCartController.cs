namespace EBuy.Web.Controllers
{
    using EBuy.Models;
    using EBuy.Services;
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

            //var shoppingCartViewModel = new ShoppingCartViewModel()
            //{
            //    Products = shoppingCart.Products
            //        .Select(x => new ShoppingCartProductViewModel()
            //        {
            //            Id = x.Id,
            //            Name = x.Name,
            //            ImageUrl = x.ImageUrl,
            //            Price = x.Price,
            //            Quantity = x.Quantity
            //        }).ToList()
            //};

            return View(new ShoppingCartViewModel { Products = products.ToList() });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ShoppingCartProductInputModel input)
        {
            var shoppingCart = this.shoppingCartService.GetShoppingCartByUsername(this.User.Identity.Name);

            await this.shoppingCartService.AddProduct(new ShoppingCartProduct()
            {
                Name = input.Name,
                ImageUrl = input.ImageUrl,
                Price = input.Price,
                Quantity = input.Quantity,
                ShoppingCartId = shoppingCart.Id
            });

            return Redirect("/ShoppingCart/Index");
        }

        public async Task<IActionResult> Remove(string id)
        {
            await this.shoppingCartService.RemoveProduct(id);

            return Redirect("/ShoppingCart/Index");
        }
    }
}
