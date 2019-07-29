namespace EBuy.Web.Areas.Admin.Models.Purchases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PurchaseViewModel
    {
        public string Username { get; set; }

        public string Address { get; set; }

        public string DateOfOrder { get; set; }

        public List<PurchaseProductViewModel> Products { get; set; }

        public decimal Amount => this.Products.Sum(x => x.Quantity * x.Price);
    }
}
