namespace EBuy.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Contracts;
    using EBuy.Data;
    using EBuy.Models;

    public class CheckoutService : ICheckoutService
    {
        private readonly EBuyDbContext context;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IPurchaseService purchaseService;

        public CheckoutService(EBuyDbContext context,
            IShoppingCartService shoppingCartService, 
            IPurchaseService purchaseService)
        {
            this.context = context;
            this.shoppingCartService = shoppingCartService;
            this.purchaseService = purchaseService;
        }

        public async Task Checkout(string username, string address)
        {
            var shoppingCartProducts = await this.shoppingCartService.GetShoppingCartProductsByUsername(username);

            if (shoppingCartProducts.Count == 0)
            {
                return;
            }

            var purchase = await this.purchaseService.Add(address, username);

            foreach (var product in shoppingCartProducts)
            {
                await this.purchaseService.AddPurchasedProduct(product, purchase);
                await this.shoppingCartService.RemoveProduct(product.Id);
            }

            await this.context.SaveChangesAsync();
        }

        public async Task CheckoutAsGuest(string cart, string address)
        {
            var purchase = await this.purchaseService.Add(address, string.Empty);
            var products = JsonConvert.DeserializeObject<List<ShoppingCartProduct>>(cart);

            foreach (var product in products)
            {
                await this.purchaseService.AddPurchasedProduct(product, purchase);
            }

            await this.context.SaveChangesAsync();
        }
    }
}
