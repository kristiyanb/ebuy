namespace EBuy.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EBuy.Data;
    using EBuy.Models;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly EBuyDbContext context;

        public CategoryService(EBuyDbContext context)
        {
            this.context = context;
        }

        public async Task<Category> GetCategoryByName(string categoryName)
            => await this.context.Categories
            .FirstOrDefaultAsync(x => x.Name == categoryName);

        public List<string> GetCategoryNames() 
            => this.context.Categories
                .Select(x => x.Name)
                .ToList();

        public async Task<List<Product>> GetProductsByCategoryName(string categoryName)
            => await this.context.Products
                .Include(x => x.Category)
                .Where(x => x.Category.Name == categoryName)
                .ToListAsync();

    }
}
