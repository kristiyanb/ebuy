namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPurchaseService
    {
        Task<IEnumerable<TViewModel>> GetAll<TViewModel>();
    }
}
