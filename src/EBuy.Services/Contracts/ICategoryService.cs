namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EBuy.Models;
    using Models;

    public interface ICategoryService
    {
        Task<List<string>> GetCategoryNames();

        Task<List<TViewModel>> GetCategories<TViewModel>();

        Task Add(CategoryDto input);

        Task<Category> GetCategoryByName(string name);
    }
}
