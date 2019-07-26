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

    public class ProductService : IProductService
    {
        private readonly EBuyDbContext context;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;

        public ProductService(EBuyDbContext context, 
            ICloudinaryService cloudinaryService, 
            IMapper mapper, 
            ICategoryService categoryService, 
            IUserService userService)
        {
            this.context = context;
            this.cloudinaryService = cloudinaryService;
            this.mapper = mapper;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        public async Task<Product> GetProductById(string id)
            => await this.context.Products
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<TViewModel> GetProductById<TViewModel>(string id)
            => await this.context.Products
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<TViewModel>> GetProductsByNameOrCategoryMatch<TViewModel>(string searchParam)
            => await this.context.Products
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Name.ToLower().Contains(searchParam.ToLower()) ||
                            x.Category.Name.ToLower().Contains(searchParam.ToLower()))
                .To<TViewModel>()
                .ToListAsync();

        public async Task<IEnumerable<TViewModel>> GetLastFiveProducts<TViewModel>()
            => await this.context.Products
                .Where(x => x.IsDeleted == false)
                .Take(5)
                .To<TViewModel>()
                .ToListAsync();

        public async Task<IEnumerable<TViewModel>> GetAll<TViewModel>(string category)
        {
            var products = this.context.Products.Where(x => x.IsDeleted == false);

            if (category != null)
            {
                products = products.Where(x => x.Category.Name == category);
            }

            var finalProducts = await products.To<TViewModel>().ToListAsync();

            return finalProducts;
        }

        public async Task<IEnumerable<TViewModel>> GetDeleted<TViewModel>()
            => await this.context.Products
               .Where(x => x.IsDeleted == true)
               .To<TViewModel>()
               .ToListAsync();

        public async Task Add(ProductDto input)
        {
            var product = this.mapper.Map<Product>(input);

            var imageUrl = await this.cloudinaryService.UploadImage(input.Image, input.Name);
            var category = await this.categoryService.GetCategoryByName(input.CategoryName);

            product.ImageUrl = imageUrl;
            product.Category = category;

            await this.context.Products.AddAsync(product);
            await this.context.SaveChangesAsync();
        }

        public async Task Edit(ProductDto input)
        {
            var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == input.Id);
            var category = await this.categoryService.GetCategoryByName(input.CategoryName);

            if (product == null || category == null)
            {
                return;
            }

            this.mapper.Map(input, product, typeof(ProductDto), typeof(Product));

            product.CategoryId = category.Id;

            if (input.Image != null)
            {
                var imageUrl = await this.cloudinaryService.UploadImage(input.Image, input.Name);

                product.ImageUrl = imageUrl;
            }

            this.context.Products.Update(product);
            await this.context.SaveChangesAsync();
        }

        public async Task Remove(string id)
        {
            var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product != null)
            {
                product.IsDeleted = true;

                this.context.Update(product);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task Restore(string id)
        {
            var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product != null)
            {
                product.IsDeleted = false;

                this.context.Update(product);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task UpdateRating(string username, string productId, string rating)
        {
            var user = await this.userService.GetUserByUserName(username);
            var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            var vote = await this.context.Votes.FirstOrDefaultAsync(x => x.UserId == user.Id && x.ProductId == productId);
            var validRating = int.TryParse(rating, out int newRating);

            if (user == null || product == null || validRating == false || (newRating < 1 && newRating > 5))
            {
                return;
            }

            if (vote == null)
            {
                product.Score += newRating;
                product.VotesCount++;

                await this.context.Votes.AddAsync(new Vote { UserId = user.Id, ProductId = productId, Score = newRating });
            }
            else
            {
                if (newRating == vote.Score)
                {
                    return;
                }

                product.Score -= vote.Score;
                product.Score += newRating;
                vote.Score = newRating;

                this.context.Votes.Update(vote);
            }

            this.context.Products.Update(product);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateProductQuantityAndSales(string name, string imageUrl, decimal price, int quantity)
        {
            var product = await this.context.Products
                .FirstOrDefaultAsync(x => x.Name == name && x.Price == price && x.ImageUrl == imageUrl);

            if (product != null)
            {
                product.InStock -= quantity;
                product.PurchasesCount += quantity;

                this.context.Products.Update(product);
                await this.context.SaveChangesAsync();
            }
        }
    }
}
