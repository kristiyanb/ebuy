namespace EBuy.Services
{
    using EBuy.Models;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        List<string> GetCategoryNames();

        List<Product> GetProductsByCategoryName(string categoryName, string orderBy);

        Category GetCategoryByName(string categoryName);

        List<Category> GetCategories();

        void Add(Category category);
    }
}
