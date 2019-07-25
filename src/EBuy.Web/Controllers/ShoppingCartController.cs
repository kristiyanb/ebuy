namespace EBuy.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    using EBuy.Common;
    using EBuy.Services.Contracts;
    using Models.ShoppingCart;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ShoppingCartProductViewModel> products;

            if (this.User.Identity.Name == null)
            {
                var cart = this.HttpContext.Session.GetString(GlobalConstants.GuestCartKey);

                if (cart == null)
                {
                    products = new List<ShoppingCartProductViewModel>();
                }
                else
                {
                    products = JsonConvert.DeserializeObject<List<ShoppingCartProductViewModel>>(cart);
                }
            }
            else
            {
                products = await this.shoppingCartService
                    .GetShoppingCartProductsByUsername<ShoppingCartProductViewModel>(this.User.Identity.Name);
            }

            return this.View(new ShoppingCartViewModel { Products = products.ToList() });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ShoppingCartProductInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.Redirect("/Products/Details/" + input.Id);
            }

            if (this.User.Identity.Name == null)
            {
                var cart = this.HttpContext.Session.GetString(GlobalConstants.GuestCartKey);
                var updatedCart = await this.shoppingCartService.AddProductToGuestCart(cart, input.Id, input.Quantity);

                this.HttpContext.Session.SetString(GlobalConstants.GuestCartKey, updatedCart);
            }
            else
            {
                await this.shoppingCartService.AddProduct(this.User.Identity.Name, input.Id, input.Quantity);
            }

            return this.Redirect("/Products/Details/" + input.Id);
        }

        public async Task<IActionResult> Remove(string id)
        {
            if (this.User.Identity.Name == null)
            {
                var cart = this.HttpContext.Session.GetString(GlobalConstants.GuestCartKey);
                var products = JsonConvert.DeserializeObject<List<ShoppingCartProductViewModel>>(cart);
                var removedProduct = products.FirstOrDefault(x => x.Id == id);

                products.Remove(removedProduct);
                this.HttpContext.Session.SetString(GlobalConstants.GuestCartKey, JsonConvert.SerializeObject(products));
            }
            else
            {
                await this.shoppingCartService.RemoveProduct(id);
            }

            return this.Redirect("/ShoppingCart/Index");
        }
    }
}
