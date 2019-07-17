namespace EBuy.Services.Contracts
{
    using EBuy.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<IEnumerable<string>> GetCategoryNames();

        Category GetCategoryByName(string categoryName);

        Task<IEnumerable<TViewModel>> GetProductsByCategoryName<TViewModel>(string categoryName, string orderBy);

        Task<IEnumerable<TViewModel>> GetCategories<TViewModel>();

        Task Add(Category category);
    }
}
