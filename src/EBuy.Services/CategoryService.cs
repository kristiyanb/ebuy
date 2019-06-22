namespace EBuy.Services
{
    using System.Collections.Generic;
    using System.Linq;
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

        public List<string> GetCategoryNames() 
            => this.context.Categories
                .Select(x => x.Name)
                .ToList();

        public List<Product> GetProductsByCategoryName(string categoryName)
            => this.context.Products
                .Include(x => x.Category)
                .Where(x => x.Category.Name == categoryName)
                .ToList();
    }
}
