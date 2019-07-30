namespace EBuy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using EBuy.Data;
    using EBuy.Models;

    public class CommentService : ICommentService
    {
        private readonly EBuyDbContext context;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public CommentService(EBuyDbContext context, 
            IUserService userService, 
            IMapper mapper)
        {
            this.context = context;
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<List<TViewModel>> GetCommentsByProductId<TViewModel>(string id)
        {
            var comments = await this.context.Comments
                .Where(x => x.ProductId == id)
                .Include(x => x.User)
                .ToListAsync();

            return this.mapper.Map<List<TViewModel>>(comments);
        }

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
