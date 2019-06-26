namespace EBuy.Services
{
    using System;
    using System.Threading.Tasks;
    using EBuy.Models;
    using EBuy.Data;
    using Microsoft.EntityFrameworkCore;

    public class CommentService : ICommentService
    {
        private readonly EBuyDbContext context;
        private readonly IUserService userService;

        public CommentService(EBuyDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task<Comment> Add(string username, string productId, string content)
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

            return comment;
        }

        public async Task<Comment> Delete(string commentId)
        {
            var comment = await this.context.Comments.FirstOrDefaultAsync(x => x.Id == commentId);

            this.context.Remove(comment);
            await this.context.SaveChangesAsync();

            return comment;
        }
    }
}
