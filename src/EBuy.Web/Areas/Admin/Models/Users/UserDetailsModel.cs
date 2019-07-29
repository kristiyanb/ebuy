namespace EBuy.Web.Areas.Admin.Models.Users
{
    using AutoMapper;

    using EBuy.Models;

    public class UserDetailsModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string RegisteredOn { get; set; }

        public string LastOnline { get; set; }

        public int PurchaseHistoryCount { get; set; }
    }
}
