namespace EBuy.Services
{
    using EBuy.Data;
    using EBuy.Models;
    using EBuy.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using System;

    public class ProductService : IProductService
    {
        private readonly EBuyDbContext context;

        public ProductService(EBuyDbContext context)
        {
            this.context = context;
        }

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

        public async Task<IEnumerable<TViewModel>> GetAll<TViewModel>()
            => await this.context.Products
                .Where(x => x.IsDeleted == false)
                .To<TViewModel>()
                .ToListAsync();

        public async Task<IEnumerable<TViewModel>> GetDeleted<TViewModel>()
           => await this.context.Products
               .Where(x => x.IsDeleted == true)
               .To<TViewModel>()
               .ToListAsync();

        public async Task Add(Product product)
        {
            await this.context.Products.AddAsync(product);
            await this.context.SaveChangesAsync();
        }

        public async Task Edit(Product updatedProduct)
        {
            var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == updatedProduct.Id);

            product.ImageUrl = updatedProduct.ImageUrl;
            product.Name = updatedProduct.Name;
            product.CategoryId = updatedProduct.CategoryId;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            product.InStock = updatedProduct.InStock;

            this.context.Products.Update(product);
            await this.context.SaveChangesAsync();
        }

        public async Task Remove(string id)
        {
            var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return;
            }

            product.IsDeleted = true;

            this.context.Update(product);
            await this.context.SaveChangesAsync();
        }

        public async Task Restore(string id)
        {
            var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return;
            }

            product.IsDeleted = false;

            this.context.Update(product);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateRating(string username, string productId, string rating)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            var vote = await this.context.Votes.FirstOrDefaultAsync(x => x.UserId == user.Id && x.ProductId == productId);
            var validRating = int.TryParse(rating, out int newRating);

            if (user == null || product == null || validRating == false)
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
    }
}
