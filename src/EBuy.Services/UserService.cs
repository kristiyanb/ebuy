namespace EBuy.Services
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using EBuy.Data;
    using EBuy.Models;

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
    }
}
