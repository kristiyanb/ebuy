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
        private readonly IUserService userService;

        public CommentService(EBuyDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<IEnumerable<TViewModel>> GetCommentsByProductId<TViewModel>(string id)
            => await this.context.Comments
                .Where(x => x.ProductId == id)
                .To<TViewModel>()
                .ToListAsync();

        public async Task Add(string username, string productId, string content)
        {
            var user = await this.userService.GetUserByUserName(username);

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
