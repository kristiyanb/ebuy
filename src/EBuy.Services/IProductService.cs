namespace EBuy.Services
{
    using EBuy.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<Product> GetProductById(string id);

        Task<List<Product>> GetProductsByNameOrCategoryMatch(string searchParam);
    }
}
