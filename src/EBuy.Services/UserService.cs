namespace EBuy.Services
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using EBuy.Data;
    using EBuy.Models;
    using System.Collections.Generic;
    using EBuy.Services.Mapping;

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
    }
}
