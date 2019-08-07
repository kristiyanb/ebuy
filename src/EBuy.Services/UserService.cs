namespace EBuy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using EBuy.Common;
    using EBuy.Data;
    using EBuy.Models;

    public class UserService : IUserService
    {
        private readonly EBuyDbContext context;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UserService(EBuyDbContext context,
            UserManager<User> userManager,
            IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<User> GetUserByUserName(string username)
            => await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

        public async Task<TViewModel> GetUserByUserName<TViewModel>(string username)
        {
            var user = await this.context.Users
                .Include(x => x.PurchaseHistory)
                .ThenInclude(x => x.Products)
                .FirstOrDefaultAsync(x => x.UserName == username);

            return this.mapper.Map<TViewModel>(user);
        }

        public async Task<List<TViewModel>> GetAll<TViewModel>()
        {
            var users = await this.context.Users
                .Where(x => x.UserName != GlobalConstants.AdminUsername)
                .Include(x => x.PurchaseHistory)
                .ToListAsync();

            return this.mapper.Map<List<TViewModel>>(users);
        }

        public async Task<bool> SetFirstName(string username, string firstName)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null) return false;

            user.FirstName = firstName;

            this.context.Update(user);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetLastName(string username, string lastName)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null) return false;

            user.LastName = lastName;

            this.context.Update(user);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddUserToRole(string username, string roleName)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (username == GlobalConstants.AdminUsername || 
                user == null || 
                !this.RoleExists(roleName))
            {
                return false;
            }

            await this.userManager.AddToRoleAsync(user, roleName);

            return true;
        }

        public async Task<bool> RemoveUserFromRole(string username, string roleName)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (username == GlobalConstants.AdminUsername || 
                user == null || 
                !this.RoleExists(roleName))
            {
                return false;
            }

            await this.userManager.RemoveFromRoleAsync(user, roleName);

            return true;
        }

        public async Task<bool> SetLastOnlineNow(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null) return false;

            user.LastOnline = DateTime.UtcNow;

            this.context.Update(user);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<IDictionary<string, List<string>>> GetUserRoleList()
        {
            var roles = new Dictionary<string, List<string>>();
            var userRoles = await this.context.UserRoles.ToListAsync();

            foreach (var userRolePair in userRoles)
            {
                var role = await this.context.Roles.FirstOrDefaultAsync(x => x.Id == userRolePair.RoleId);

                if (role.NormalizedName == GlobalConstants.AdminNormalizedRoleName || 
                    role.NormalizedName == GlobalConstants.UserNormalizedRoleName)
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