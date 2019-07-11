namespace EBuy.Services
{
    using EBuy.Models;

    public interface IUserService
    {
        User GetUserByUserName(string username);
    }
}
