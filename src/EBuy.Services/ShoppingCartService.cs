namespace EBuy.Services
{
    using EBuy.Data;
    using EBuy.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly EBuyDbContext context;

        public ShoppingCartService(EBuyDbContext context)
        {
            this.context = context;
        }

        public void AddProduct(ShoppingCartProduct product)
        {
            this.context.Add(product);
            this.context.SaveChanges();
        }

        public bool RemoveProduct(string id)
        {
            var product = this.context.ShoppingCartProducts.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return false;
            }

            this.context.ShoppingCartProducts.Remove(product);
            this.context.SaveChanges();

            return true;
        }

        public IQueryable<ShoppingCart> GetShoppingCartByUsername(string username)
            => this.context.ShoppingCarts
            .Include(x => x.Products)
            .Include(x => x.User)
            .Where(x => x.User.UserName == username);
    }
}
