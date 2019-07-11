namespace EBuy.Services
{
    using EBuy.Models;
    using System.Linq;

    public interface IShoppingCartService
    {
        void AddProduct(ShoppingCartProduct product);

        bool RemoveProduct(string id);

        IQueryable<ShoppingCart> GetShoppingCartByUsername(string username);
    }
}
