namespace EBuy.Services.Contracts
{
    using EBuy.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task Add(string userId, string productId, string content);

        Comment Delete(string commentId);

        Task<IEnumerable<TViewModel>> GetCommentsByProductId<TViewModel>(string id);
    }
}
