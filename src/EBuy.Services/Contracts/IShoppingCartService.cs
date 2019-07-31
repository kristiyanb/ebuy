namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EBuy.Models;

    public interface IShoppingCartService
    {
        Task<bool> AddProduct(string username, string id, int quantity);

        Task<bool> RemoveProduct(string id);

        Task<List<ShoppingCartProduct>> GetShoppingCartProductsByUsername(string username);

        Task<List<TViewModel>> GetShoppingCartProductsByUsername<TViewModel>(string username);

        Task<string> AddProductToGuestCart(string cart, string id, int quantity);
    }
}
