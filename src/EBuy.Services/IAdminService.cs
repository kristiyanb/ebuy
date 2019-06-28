namespace EBuy.Services
{
    using EBuy.Models;
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task AddProduct(Product product);

        Task AddCategory(Category category);

        Task AddCompany(Company company);
    }
}
