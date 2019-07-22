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

    public class UserService : IUserService
    {
        private readonly EBuyDbContext context;

        public UserService(EBuyDbContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserByUserName(string username) 
            => await this.context.Users
                .FirstOrDefaultAsync(x => x.UserName == username);

        public async Task<IEnumerable<TViewModel>> GetAll<TViewModel>() 
            => await this.context.Users
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
    }
}
