namespace EBuy.Services
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using EBuy.Data;
    using EBuy.Services.Mapping;
    using System.Threading.Tasks;
    using Contracts;
    using EBuy.Services.Models;
    using AutoMapper;
    using EBuy.Models;

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
            var products = this.context.Products
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Category.Name == categoryName);

            if (orderBy != null)
            {
                switch (orderBy)
                {
                    case "Name": products = products.OrderBy(x => x.Name); break;
                    case "Rating": products = products.OrderByDescending(x => (x.Score / (x.VotesCount == 0 ? 1 : x.VotesCount))); break;
                    case "Price": products = products.OrderBy(x => x.Price); break;
                    case "PriceDescending": products = products.OrderByDescending(x => x.Price); break;
                    default: break;
                }
            }

            return await products.To<TViewModel>().ToListAsync();
        }

        public async Task<IEnumerable<TViewModel>> GetCategories<TViewModel>()
            => await this.context.Categories
                .To<TViewModel>()
                .ToListAsync();

        public async Task Add(CategoryDto input)
        {
            var category = this.mapper.Map<Category>(input);
            var imageUrl = await this.cloudinaryService.UploadImage(input.Image, input.Name + "-image");

            category.ImageUrl = imageUrl;

            await this.context.Categories.AddAsync(category);
            await this.context.SaveChangesAsync();
        }
    }
}
