namespace EBuy.Services
{
    using EBuy.Models;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        List<string> GetCategoryNames();

        List<Product> GetProductsByCategoryName(string categoryName);
    }
}
