namespace EBuy.Services
{
    using EBuy.Data;
    using EBuy.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
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

        //public async Task<Comment> AddComment(string userId, string productId, string content, DateTime lastModified)
        //{
        //    var comment = new Comment()
        //    {
        //        Content = content,
        //        UserId = userId,
        //        ProductId = productId,
        //        LastModified = lastModified,
        //    };

        //    await this.context.Comments.AddAsync(comment);
        //    await this.context.SaveChangesAsync();

        //    return comment;
        //}
    }
}
