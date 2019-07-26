namespace EBuy.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using EBuy.Data;
    using EBuy.Models;
    using Mapping;
    using Models;

    public class CategoryService : ICategoryService
    {
        private readonly EBuyDbContext context;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IMapper mapper;

        public CategoryService(EBuyDbContext context, ICloudinaryService cloudinaryService, IMapper mapper)
        {
            this.context = context;
            this.cloudinaryService = cloudinaryService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<string>> GetCategoryNames()
            => await this.context.Categories
                .Select(x => x.Name)
                .ToListAsync();

        public async Task<IEnumerable<TViewModel>> GetProductsByCategoryName<TViewModel>(string categoryName, string orderBy)
        {
            var productsFromDb = this.context.Products
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Category.Name == categoryName);

            if (orderBy != null)
            {
                switch (orderBy)
                {
                    case "Name": productsFromDb = productsFromDb.OrderBy(x => x.Name);
                        break;
                    case "Rating": productsFromDb = productsFromDb.OrderByDescending(x => x.Score / (x.VotesCount == 0 ? 1 : x.VotesCount));
                        break;
                    case "Price": productsFromDb = productsFromDb.OrderBy(x => x.Price);
                        break;
                    case "PriceDescending": productsFromDb = productsFromDb.OrderByDescending(x => x.Price);
                        break;
                    default:
                        break;
                }
            }

            var products = await productsFromDb
                .To<TViewModel>()
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<TViewModel>> GetCategories<TViewModel>()
            => await this.context.Categories
                .To<TViewModel>()
                .ToListAsync();

        public async Task Add(CategoryDto input)
        {
            var category = this.mapper.Map<Category>(input);
            var imageUrl = await this.cloudinaryService.UploadImage(input.Image, input.Name);

            category.ImageUrl = imageUrl;

            await this.context.Categories.AddAsync(category);
            await this.context.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryByName(string name)
            => await this.context.Categories
                .FirstOrDefaultAsync(x => x.Name == name);
    }
}
