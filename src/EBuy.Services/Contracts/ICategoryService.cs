namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EBuy.Models;
    using Models;

    public interface ICategoryService
    {
        Task<IEnumerable<string>> GetCategoryNames();

        Task<IEnumerable<TViewModel>> GetProductsByCategoryName<TViewModel>(string categoryName, string orderBy);

        Task<IEnumerable<TViewModel>> GetCategories<TViewModel>();

        Task Add(CategoryDto input);

        Task<Category> GetCategoryByName(string name);
    }
}
