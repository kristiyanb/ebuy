namespace EBuy.Services
{
    using Contracts;
    using EBuy.Data;
    using EBuy.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CheckoutService : ICheckoutService
    {
        private readonly EBuyDbContext context;
        private readonly IUserService userService;

        public CheckoutService(EBuyDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<bool> Checkout(string username, string address)
        {
            var shoppingCartProducts = this.context.ShoppingCartProducts.Where(x => x.ShoppingCart.User.UserName == username).ToList();

            if (shoppingCartProducts.Count == 0)
            {
                return false;
            }

            var user = await this.userService.GetUserByUserName(username);

            var purchase = new Purchase
            {
                Address = address,
                DateOfOrder = DateTime.UtcNow,
                UserId = user.Id,
            };

            await this.context.AddAsync(purchase);

            foreach (var product in shoppingCartProducts)
            {
                var purchasedProduct = new PurchasedProduct
                {
                    Name = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = product.Quantity,
                    Purchase = purchase
                };

                await this.context.PurchasedProducts.AddAsync(purchasedProduct);
                this.context.Remove(product);
            }

            await this.context.SaveChangesAsync();

            return true;
        }
    }
}
