namespace EBuy.Web.Areas.Admin.Models.Purchases
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using EBuy.Models;
    using EBuy.Services.Mapping;

    public class PurchaseViewModel : IMapFrom<Purchase>
    {
        public string Username { get; set; }

        public string Address { get; set; }

        public DateTime DateOfOrder { get; set; }

        public List<PurchaseProductViewModel> Products { get; set; }

        public decimal Amount => this.Products.Sum(x => x.Quantity * x.Price);
    }
}
