namespace EBuy.Services
{
    using EBuy.Models;
    using System.Collections.Generic;

    public interface IProductService
    {
        Product GetProductById(string id);

        List<Product> GetProductsByNameOrCategoryMatch(string searchParam);

        List<Product> GetLastFiveProducts();

        void Add(Product product);
    }
}
