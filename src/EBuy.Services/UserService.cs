namespace EBuy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Mapping;
    using EBuy.Data;
    using EBuy.Models;

    public class UserService : IUserService
    {
        private readonly EBuyDbContext context;
        private readonly UserManager<User> userManager;

        public UserService(EBuyDbContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<User> GetUserByUserName(string username)
            => await this.context.Users
                .FirstOrDefaultAsync(x => x.UserName == username);

        public async Task<TViewModel> GetUserByUserName<TViewModel>(string username)
            => await this.context.Users
                .Where(x => x.UserName == username)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<TViewModel>> GetAll<TViewModel>()
            => await this.context.Users
                .Where(x => x.UserName != "admin")
                .To<TViewModel>()
                .ToListAsync();

        public async Task<IEnumerable<TViewModel>> GetPurchaseHistory<TViewModel>(string username)
            => await this.context.Purchases
                .Where(x => x.User.UserName == username)
                .To<TViewModel>()
                .ToListAsync();

        public async Task SetFirstName(string username, string firstName)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            user.FirstName = firstName;

            this.context.Update(user);
            await this.context.SaveChangesAsync();
        }

        public async Task SetLastName(string username, string lastName)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            user.LastName = lastName;

            this.context.Update(user);
            await this.context.SaveChangesAsync();
        }

        public async Task AddUserToRole(string username, string roleName)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (username != "admin" && user != null && this.RoleExists(roleName))
            {
                await this.userManager.AddToRoleAsync(user, roleName);
            }
        }

        public async Task RemoveUserFromRole(string username, string roleName)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (username != "admin" && user != null && this.RoleExists(roleName))
            {
                await this.userManager.RemoveFromRoleAsync(user, roleName);
            }
        }

        public async Task SetLastOnlineNow(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user != null)
            {
                user.LastOnline = DateTime.UtcNow;

                this.context.Update(user);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task<IDictionary<string, List<string>>> GetUserRoleList()
        {
            var roles = new Dictionary<string, List<string>>();
            var userRoles = await this.context.UserRoles.ToListAsync();

            foreach (var userRolePair in userRoles)
            {
                var role = await this.context.Roles.FirstOrDefaultAsync(x => x.Id == userRolePair.RoleId);

                if (role.Name.ToLower() == "administrator" || role.Name.ToLower() == "user")
                {
                    continue;
                }

                var user = await this.context.Users.FirstOrDefaultAsync(x => x.Id == userRolePair.UserId);

                if (!roles.ContainsKey(role.Name))
                {
                    roles[role.Name] = new List<string>();
                }

                var userInfo = $"{user.FirstName} {user.LastName} ({user.Email})";

                roles[role.Name].Add(userInfo);
            }

            return roles;
        }

        private bool RoleExists(string roleName)
        {
            var role = this.context.Roles.FirstOrDefault(x => x.Name == roleName);

            return role != null;
        }
    }
}