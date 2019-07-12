namespace EBuy.Services.Contracts
{
    using EBuy.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<TViewModel> GetProductById<TViewModel>(string id);

        Task<IEnumerable<TViewModel>> GetProductsByNameOrCategoryMatch<TViewModel>(string searchParam);

        Task<IEnumerable<TViewModel>> GetLastFiveProducts<TViewModel>();

        Task<IEnumerable<TViewModel>> GetAll<TViewModel>();

        Task Add(Product product);
    }
}
