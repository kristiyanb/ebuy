namespace EBuy.Services
{
    using EBuy.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShoppingCartService
    {
        Task AddProduct(ShoppingCartProduct product);

        Task RemoveProduct(string id);

        ShoppingCart GetShoppingCartByUsername(string username);

        Task<IEnumerable<TViewModel>> GetShoppingCartProductsByUsername<TViewModel>(string username);
    }
}
