namespace EBuy.Services
{
    using System.Threading.Tasks;
    using EBuy.Data;
    using EBuy.Models;

    public class AdminService : IAdminService
    {
        private readonly EBuyDbContext context;

        public AdminService(EBuyDbContext context)
        {
            this.context = context;
        }

        public async Task AddCategory(Category category)
        {
            await this.context.Categories.AddAsync(category);
            await this.context.SaveChangesAsync();
        }

        public async Task AddCompany(Company company)
        {
            await this.context.Companies.AddAsync(company);
            await this.context.SaveChangesAsync();
        }

        public async Task AddProduct(Product product)
        {
            await this.context.Products.AddAsync(product);
            await this.context.SaveChangesAsync();
        }
    }
}
