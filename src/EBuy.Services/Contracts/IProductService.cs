namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using Models;
    using EBuy.Models;

    public interface IProductService
    {
        Task<Product> GetProductById(string id);

        Task<TViewModel> GetProductById<TViewModel>(string id);

        Task<List<TViewModel>> GetProductsByCategoryName<TViewModel>(string categoryName, string orderBy);

        Task<List<TViewModel>> GetProductsByNameOrCategoryMatch<TViewModel>(string query);

        Task<List<TViewModel>> GetLastFiveProducts<TViewModel>();

        Task<List<TViewModel>> GetAll<TViewModel>(string category);

        Task<List<TViewModel>> GetDeleted<TViewModel>();

        Task<bool> Add(ProductDto input);

        Task<bool> Edit(ProductDto input);

        Task<bool> Remove(string id);

        Task<bool> Restore(string id);

        Task<bool> UpdateRating(string username, string productId, string rating);
    }
}
