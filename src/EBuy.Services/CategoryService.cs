namespace EBuy.Services
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using EBuy.Data;
    using EBuy.Models;
    using EBuy.Services.Mapping;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly EBuyDbContext context;

        public CategoryService(EBuyDbContext context)
        {
            this.context = context;
        }

        public Category GetCategoryByName(string categoryName)
            => this.context.Categories
                .FirstOrDefault(x => x.Name == categoryName);

        public List<string> GetCategoryNames()
            => this.context.Categories
                .Select(x => x.Name)
                .ToList();

        public async Task<IEnumerable<TViewModel>> GetProductsByCategoryName<TViewModel>(string categoryName, string orderBy)
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

            return await products.To<TViewModel>().ToListAsync();
        }

        public async Task<IEnumerable<TViewModel>> GetCategories<TViewModel>()
            => await this.context.Categories
                .Include(x => x.Products)
                .To<TViewModel>()
                .ToListAsync();

        public async Task Add(Category category)
        {
            await this.context.Categories.AddAsync(category);
            await this.context.SaveChangesAsync();
        }
    }
}
