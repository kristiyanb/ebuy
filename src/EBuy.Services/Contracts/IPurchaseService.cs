namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EBuy.Models;

    public interface IPurchaseService
    {
        Task<List<TViewModel>> GetAll<TViewModel>();

        Task<Purchase> Add(string address, string username);

        Task AddPurchasedProduct(ShoppingCartProduct product, Purchase purchase);

        Task<List<TViewModel>> GetUserPurchaseHistory<TViewModel>(string username);
    }
}
