namespace EBuy.Web.Models.Users
{
    using EBuy.Models;
    using EBuy.Services.Mapping;

    public class UserViewModel : IMapFrom<User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
