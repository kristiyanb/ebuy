namespace EBuy.Services
{
    using System;
    using EBuy.Data;
    using EBuy.Models;
    using System.Linq;
    using System.Threading.Tasks;

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

            this.context.Add(comment);
            this.context.SaveChanges();

            return comment;
        }

        public Comment Delete(string commentId)
        {
            var comment = this.context.Comments.FirstOrDefault(x => x.Id == commentId);

            this.context.Remove(comment);
            this.context.SaveChanges();

            return comment;
        }
    }
}
