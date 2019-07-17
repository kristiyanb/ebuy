namespace EBuy.Services.Contracts
{
    using EBuy.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IShoppingCartService
    {
        Task AddProduct(ShoppingCartProduct product);

        Task RemoveProduct(string id);

        Task<string> GetShoppingCartIdByUsername(string username);

        Task<IEnumerable<TViewModel>> GetShoppingCartProductsByUsername<TViewModel>(string username);
    }
}
