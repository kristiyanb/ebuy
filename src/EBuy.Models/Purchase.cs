namespace EBuy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Purchase
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime DateOfOrder { get; set; }

        public ICollection<PurchasedProduct> Products { get; set; }
    }
}
