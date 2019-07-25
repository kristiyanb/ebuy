namespace EBuy.Web.ViewComponents
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

            return this.View(new ShoppingCartDropdownViewModel { Products = products.ToList() });
        }
    }
}
