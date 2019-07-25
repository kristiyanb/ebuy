namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using Models;

    public interface IProductService
    {
        Task<TViewModel> GetProductById<TViewModel>(string id);

        Task<IEnumerable<TViewModel>> GetProductsByNameOrCategoryMatch<TViewModel>(string searchParam);

        Task<IEnumerable<TViewModel>> GetLastFiveProducts<TViewModel>();

        Task<IEnumerable<TViewModel>> GetAll<TViewModel>();

        Task<IEnumerable<TViewModel>> GetDeleted<TViewModel>();

        Task Add(ProductDto input);

        Task Edit(ProductDto input);

        Task Remove(string id);

        Task Restore(string id);

        Task UpdateRating(string username, string productId, string rating);
    }
}
