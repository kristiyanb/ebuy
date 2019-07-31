namespace EBuy.Web.Areas.Admin.Models.Purchases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PurchaseViewModel
    {
        public string UserEmail { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string Address { get; set; }

        public string DateOfOrder { get; set; }

        public List<PurchaseProductViewModel> Products { get; set; }

        public decimal Amount => this.Products.Sum(x => x.Quantity * x.Price);

        public string ClientInfo 
            => string.IsNullOrEmpty(this.UserEmail) ? 
              "Guest User" 
            : $"{this.UserFirstName} {this.UserLastName} ({this.UserEmail})";
    }
}
