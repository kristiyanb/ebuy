namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EBuy.Models;

    public interface IShoppingCartService
    {
        Task AddProduct(string username, string id, int quantity);

        Task RemoveProduct(string id);

        Task<IEnumerable<ShoppingCartProduct>> GetShoppingCartProductsByUsername(string username);

        Task<IEnumerable<TViewModel>> GetShoppingCartProductsByUsername<TViewModel>(string username);

        Task<string> AddProductToGuestCart(string cart, string id, int quantity);
    }
}
