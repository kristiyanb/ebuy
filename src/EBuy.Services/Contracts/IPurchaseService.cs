namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPurchaseService
    {
        Task<List<TViewModel>> GetAll<TViewModel>();

        Task<List<TViewModel>> GetUserPurchaseHistory<TViewModel>(string username);
    }
}
