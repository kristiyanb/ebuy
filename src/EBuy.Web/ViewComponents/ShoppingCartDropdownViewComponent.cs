namespace EBuy.Web.ViewComponents
{
    using EBuy.Services.Contracts;
    using EBuy.Web.Models.ShoppingCart;
    using Microsoft.AspNetCore.Mvc;
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
            var products = await this.shoppingCartService
                .GetShoppingCartProductsByUsername<ShoppingCartProductViewModel>(this.User.Identity.Name);

            return View(new ShoppingCartDropdownViewModel { Products = products.ToList() });
        }
    }
}
