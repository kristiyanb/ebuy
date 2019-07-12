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

        public ShoppingCartService(EBuyDbContext context)
        {
            this.context = context;
        }

        public async Task AddProduct(ShoppingCartProduct product)
        {
            await this.context.AddAsync(product);
            await this.context.SaveChangesAsync();
        }

        public async Task RemoveProduct(string id)
        {
            var product = await this.context.ShoppingCartProducts.FirstOrDefaultAsync(x => x.Id == id);

            this.context.ShoppingCartProducts.Remove(product);
            await this.context.SaveChangesAsync();
        }

        public ShoppingCart GetShoppingCartByUsername(string username)
            => this.context.ShoppingCarts
                .Where(x => x.User.UserName == username)
                .FirstOrDefault();

        public async Task<IEnumerable<TViewModel>> GetShoppingCartProductsByUsername<TViewModel>(string username)
            => await this.context.ShoppingCartProducts
                .Where(x => x.ShoppingCart.User.UserName == username)
                .To<TViewModel>()
                .ToListAsync();
    }
}
