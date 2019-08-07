namespace EBuy.Web.Areas.Admin.Models.Users
{
    using System.Collections.Generic;

    using Purchases;

    public class UserDataModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string RegisteredOn { get; set; }

        public string LastOnline { get; set; }

        public int PurchaseHistoryCount { get; set; }

        public List<PurchaseViewModel> PurchaseHistory { get; set; }
    }
}
