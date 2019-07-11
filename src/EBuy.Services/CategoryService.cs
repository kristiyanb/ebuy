namespace EBuy.Services
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using EBuy.Data;
    using EBuy.Models;

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

        public List<Product> GetProductsByCategoryName(string categoryName, string orderBy)
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

            return products.ToList();
        }

        public List<Category> GetCategories()
            => this.context.Categories
                .Include(x => x.Products)
                .ToList();

        public void Add(Category category)
        {
            this.context.Categories.AddAsync(category);
            this.context.SaveChangesAsync();
        }
    }
}
