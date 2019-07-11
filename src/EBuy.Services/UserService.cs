namespace EBuy.Services
{
    using System.Linq;
    using EBuy.Data;
    using EBuy.Models;

    public class UserService : IUserService
    {
        private readonly EBuyDbContext context;

        public UserService(EBuyDbContext context)
        {
            this.context = context;
        }

        public User GetUserByUserName(string username) 
            => this.context.Users
            .FirstOrDefault(x => x.UserName == username);
    }
}
