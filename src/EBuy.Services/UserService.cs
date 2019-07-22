namespace EBuy.Services
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using EBuy.Data;
    using EBuy.Models;
    using System.Collections.Generic;
    using EBuy.Services.Mapping;
    using Contracts;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using System;

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
            var user = await this.GetUserByUserName(username);

            if (username != "admin" && user != null)
            {
                await this.userManager.AddToRoleAsync(user, roleName);
            }
        }

        public async Task RemoveUserFromRole(string username, string roleName)
        {
            var user = await this.GetUserByUserName(username);

            if (username != "admin" && user != null)
            {
                await this.userManager.RemoveFromRoleAsync(user, roleName);
            }
        }

        public async Task SetLastOnlineNow(string username)
        {
            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null)
            {
                return;
            }

            user.LastOnline = DateTime.UtcNow;

            this.context.Update(user);
            await this.context.SaveChangesAsync();
        }
    }
}
