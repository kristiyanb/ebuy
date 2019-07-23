namespace EBuy.Web.ViewComponents
{
    using EBuy.Services.Contracts;
    using EBuy.Web.Models.ShoppingCart;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShoppingCartDropdownViewComponent : ViewComponent
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartDropdownViewComponent(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<ShoppingCartProductViewModel> products;

            if (this.User.Identity.Name == null)
            {
                var cart = HttpContext.Session.GetString("cart");

                if (cart != null)
                {
                    products = JsonConvert.DeserializeObject<List<ShoppingCartProductViewModel>>(cart);
                }
                else
                {
                    products = new List<ShoppingCartProductViewModel>();
                }
            }
            else
            {
                products = await this.shoppingCartService
                    .GetShoppingCartProductsByUsername<ShoppingCartProductViewModel>(this.User.Identity.Name);
            }

            return View(new ShoppingCartDropdownViewModel { Products = products.ToList() });
        }
    }
}
