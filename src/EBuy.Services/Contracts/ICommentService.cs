namespace EBuy.Services.Contracts
{
    using EBuy.Models;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task<Comment> Add(string userId, string productId, string content);

        Comment Delete(string commentId);
    }
}
