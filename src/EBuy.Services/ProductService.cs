namespace EBuy.Services
{
    using EBuy.Data;
    using EBuy.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly EBuyDbContext context;

        public ProductService(EBuyDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> GetProductById(string id)
            => await this.context.Products
            .Include(x => x.Comments)
            .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<Product>> GetProductsByNameOrCategoryMatch(string searchParam)
        {
            var products = await this.context.Products
                .Include(x => x.Category)
                .Where(x => x.Name.ToLower().Contains(searchParam.ToLower()) ||
                            x.Category.Name.ToLower().Contains(searchParam.ToLower()))
                .ToListAsync();

            return products;
        }

        public Task<List<Product>> GetLastFiveProducts() => this.context.Products.Take(5).ToListAsync();
    }
}
