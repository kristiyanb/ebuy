namespace EBuy.Services
{
    using EBuy.Data;
    using EBuy.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService : IProductService
    {
        private readonly EBuyDbContext context;

        public ProductService(EBuyDbContext context)
        {
            this.context = context;
        }

        public Product GetProductById(string id)
            => this.context.Products
            .Include(x => x.Comments)
            .ThenInclude(x => x.User)
            .FirstOrDefault(x => x.Id == id);

        public List<Product> GetProductsByNameOrCategoryMatch(string searchParam)
        {
            var products = this.context.Products
                .Include(x => x.Category)
                .Where(x => x.Name.ToLower().Contains(searchParam.ToLower()) ||
                            x.Category.Name.ToLower().Contains(searchParam.ToLower()))
                .ToList();

            return products;
        }

        public List<Product> GetLastFiveProducts() => this.context.Products.Take(5).ToList();

        public void Add(Product product)
        {
            this.context.Products.Add(product);
            this.context.SaveChanges();
        }
    }
}
