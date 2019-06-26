namespace EBuy.Services
{
    using EBuy.Data;
    using EBuy.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly EBuyDbContext context;

        public ProductService(EBuyDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> GetProductById(string id)
            => await this.context.Products
            .Include(x => x.Comments)
            .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
