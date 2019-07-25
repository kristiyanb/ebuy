namespace EBuy.Services.Contracts
{
    using EBuy.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<IEnumerable<string>> GetCategoryNames();

        Task<IEnumerable<TViewModel>> GetProductsByCategoryName<TViewModel>(string categoryName, string orderBy);

        Task<IEnumerable<TViewModel>> GetCategories<TViewModel>();

        Task Add(CategoryDto input);
    }
}
