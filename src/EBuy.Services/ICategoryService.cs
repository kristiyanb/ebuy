namespace EBuy.Services
{
    using EBuy.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        List<string> GetCategoryNames();

        Task<List<Product>> GetProductsByCategoryName(string categoryName, string orderBy);

        Task<Category> GetCategoryByName(string categoryName);

        Task<List<Category>> GetCategories();

        Task Add(Category category);
    }
}
