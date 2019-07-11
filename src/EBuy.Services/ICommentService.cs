namespace EBuy.Services
{
    using EBuy.Models;

    public interface ICommentService
    {
        Comment Add(string userId, string productId, string content);

        Comment Delete(string commentId);
    }
}
