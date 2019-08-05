namespace EBuy.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.PurchaseHistory = new HashSet<Purchase>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public DateTime LastOnline { get; set; }

        public DateTime RegisteredOn { get; set; }

        public ICollection<Purchase> PurchaseHistory { get; set; }
    }
}
