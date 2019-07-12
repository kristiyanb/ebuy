namespace EBuy.Services.Contracts
{
    using EBuy.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<User> GetUserByUserName(string username);

        Task<IEnumerable<TViewModel>> GetAll<TViewModel>();
    }
}
