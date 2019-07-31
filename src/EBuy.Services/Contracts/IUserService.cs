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

        Task<bool> SetFirstName(string username, string firstName);

        Task<bool> SetLastName(string username, string lastName);

        Task<bool> AddUserToRole(string username, string roleName);

        Task<bool> RemoveUserFromRole(string username, string roleName);

        Task<bool> SetLastOnlineNow(string username);

        Task<IDictionary<string, List<string>>> GetUserRoleList();
    }
}
