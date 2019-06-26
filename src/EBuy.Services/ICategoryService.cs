namespace EBuy.Services
{
    using EBuy.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        List<string> GetCategoryNames();

        Task<List<Product>> GetProductsByCategoryName(string categoryName);

        Task<Category> GetCategoryByName(string categoryName);
    }
}
