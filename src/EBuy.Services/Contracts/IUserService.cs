﻿namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<TViewModel> GetUserByUserName<TViewModel>(string username);

        Task<IEnumerable<TViewModel>> GetAll<TViewModel>();

        Task<IEnumerable<TViewModel>> GetPurchaseHistory<TViewModel>(string username);

        Task SetFirstName(string username, string firstName);

        Task SetLastName(string username, string lastName);

        Task AddUserToRole(string username, string roleName);

        Task RemoveUserFromRole(string username, string roleName);

        Task SetLastOnlineNow(string username);

        Task<IDictionary<string, List<string>>> GetUserRoleList();
    }
}
