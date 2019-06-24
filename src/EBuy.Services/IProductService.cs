namespace EBuy.Services
{
    using EBuy.Models;
    using System;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<Product> GetProductById(string id);

        //Task<Comment> AddComment(string userId, string productId, string content, DateTime lastModified);
    }
}
