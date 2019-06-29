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

        public async Task<List<Product>> GetProductsByCategoryName(string categoryName, string orderBy)
        {
            var products = this.context.Products
                .Include(x => x.Category)
                .Where(x => x.Category.Name == categoryName);

            if (orderBy != null)
            {
                switch (orderBy)
                {
                    case "Name": products = products.OrderBy(x => x.Name); break;
                    case "Rating": products = products.OrderByDescending(x => (x.Score / x.VotesCount)); break;
                    case "Price": products = products.OrderBy(x => x.Price); break;
                    case "PriceDescending": products = products.OrderByDescending(x => x.Price); break;
                    default: break;
                }
            }

            return await products.ToListAsync();
        }


        public async Task<List<Category>> GetCategories()
            => await this.context.Categories
                .Include(x => x.Products)
                .ToListAsync();
    }
}
