namespace EBuy.Services
{
    using EBuy.Models;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task<Comment> Add(string userId, string productId, string content);

        Task<Comment> Delete(string commentId);
    }
}
