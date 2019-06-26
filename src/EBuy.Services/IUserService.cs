namespace EBuy.Services
{
    using EBuy.Models;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<User> GetUserByUserName(string username);
    }
}
