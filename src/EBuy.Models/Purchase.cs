namespace EBuy.Models
{
    using System;
    using System.Collections.Generic;

    public class Purchase
    {
        public Purchase()
        {
            this.Products = new List<PurchasedProduct>();
        }

        public string Id { get; set; }

        public string Address { get; set; }

        public decimal Amount { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime DateOfOrder { get; set; }

        public decimal Discount { get; set; }

        public ICollection<PurchasedProduct> Products { get; set; }
    }
}
