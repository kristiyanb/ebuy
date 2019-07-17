namespace EBuy.Services
{
    using EBuy.Data;
    using EBuy.Models;
    using EBuy.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly EBuyDbContext context;
        private readonly IUserService userService;

        public ShoppingCartService(EBuyDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task AddProduct(string username, string id, int quantity)
        {
            var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == id);
            var shoppingCart = await this.GetShoppingCart(username);

            var cartProduct = new ShoppingCartProduct
            {
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Quantity = quantity,
                ShoppingCartId = shoppingCart.Id
            };

            await this.context.AddAsync(cartProduct);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveProduct(string id)
        {
            var product = await this.context.ShoppingCartProducts.FirstOrDefaultAsync(x => x.Id == id);

            this.context.ShoppingCartProducts.Remove(product);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TViewModel>> GetShoppingCartProductsByUsername<TViewModel>(string username)
            => await this.context.ShoppingCartProducts
                .Where(x => x.ShoppingCart.User.UserName == username)
                .To<TViewModel>()
                .ToListAsync();

        private async Task<ShoppingCart> GetShoppingCart(string username)
        {
            var shoppingCart = await this.context.ShoppingCarts.FirstOrDefaultAsync(x => x.User.UserName == username);

            if (shoppingCart != null)
            {
                return shoppingCart;
            }

            var user = await this.userService.GetUserByUserName(username);

            await this.context.ShoppingCarts.AddAsync(new ShoppingCart { UserId = user.Id });
            await this.context.SaveChangesAsync();

            return await this.context.ShoppingCarts.FirstOrDefaultAsync(x => x.User.UserName == username);
        }
    }
}
