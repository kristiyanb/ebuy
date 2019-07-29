namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EBuy.Models;

    public interface IUserService
    {
        Task<User> GetUserByUserName(string username);

        Task<TViewModel> GetUserByUserName<TViewModel>(string username);

        Task<List<TViewModel>> GetAll<TViewModel>();

        Task SetFirstName(string username, string firstName);

        Task SetLastName(string username, string lastName);

        Task AddUserToRole(string username, string roleName);

        Task RemoveUserFromRole(string username, string roleName);

        Task SetLastOnlineNow(string username);

        Task<IDictionary<string, List<string>>> GetUserRoleList();
    }
}
