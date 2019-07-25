namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;

    public interface ICategoryService
    {
        Task<IEnumerable<string>> GetCategoryNames();

        Task<IEnumerable<TViewModel>> GetProductsByCategoryName<TViewModel>(string categoryName, string orderBy);

        Task<IEnumerable<TViewModel>> GetCategories<TViewModel>();

        Task Add(CategoryDto input);
    }
}
