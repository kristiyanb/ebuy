namespace EBuy.Web.Controllers
{
    using EBuy.Services.Contracts;
    using EBuy.Web.Models.ShoppingCart;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
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
            IEnumerable<ShoppingCartProductViewModel> products;

            if (this.User.Identity.Name == null)
            {
                var cart = HttpContext.Session.GetString("cart");

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

            return View(new ShoppingCartViewModel { Products = products.ToList() });
        }

        [HttpPost]
        public async Task<IActionResult> Add(ShoppingCartProductInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Products/Details/" + input.Id);
            }

            if (this.User.Identity.Name == null)
            {
                //HttpContext.Response.Cookies.Append("", "", new CookieOptions() { Expires = D})

                var cart = HttpContext.Session.GetString("cart");
                var updatedCart = await this.shoppingCartService.AddProductToGuestCart(cart, input.Id, input.Quantity);

                HttpContext.Session.SetString("cart", updatedCart);
            }
            else
            {
                await this.shoppingCartService.AddProduct(User.Identity.Name, input.Id, input.Quantity);
            }

            return Redirect("/Products/Details/" + input.Id);
        }

        public async Task<IActionResult> Remove(string id)
        {
            if (this.User.Identity.Name == null)
            {
                var cart = HttpContext.Session.GetString("cart");
                var products = JsonConvert.DeserializeObject<List<ShoppingCartProductViewModel>>(cart);
                var removedProduct = products.FirstOrDefault(x => x.Id == id);
                products.Remove(removedProduct);
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(products));
            }
            else
            {
                await this.shoppingCartService.RemoveProduct(id);
            }

            return Redirect("/ShoppingCart/Index");
        }
    }
}
