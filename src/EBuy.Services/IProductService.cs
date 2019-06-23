namespace EBuy.Services
{
    using EBuy.Models;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<Product> GetProductById(string id);
    }
}
