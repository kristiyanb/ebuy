namespace EBuy.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public User()
        {
            this.PurchaseHistory = new HashSet<Purchase>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime LastOnline { get; set; }

        public DateTime RegisteredOn { get; set; }

        public ICollection<Purchase> PurchaseHistory { get; set; }
    }
}
