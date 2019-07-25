namespace EBuy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using EBuy.Data;
    using EBuy.Models;
    using Mapping;

    public class CommentService : ICommentService
    {
        private readonly EBuyDbContext context;

        public CommentService(EBuyDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TViewModel>> GetCommentsByProductId<TViewModel>(string id)
            => await this.context.Comments
                .Where(x => x.ProductId == id)
                .To<TViewModel>()
                .ToListAsync();

        public async Task Add(string username, string productId, string content)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            var comment = new Comment()
            {
                Content = content,
                UserId = user.Id,
                ProductId = productId,
                LastModified = DateTime.UtcNow
            };

            await this.context.AddAsync(comment);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string commentId)
        {
            var comment = await this.context.Comments.FirstOrDefaultAsync(x => x.Id == commentId);

            this.context.Remove(comment);
            await this.context.SaveChangesAsync();
        }
    }
}
