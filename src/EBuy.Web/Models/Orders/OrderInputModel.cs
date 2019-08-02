﻿namespace EBuy.Web.Models.Orders
{
    using System.Collections.Generic;

    public class OrderInputModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string PaymentMethod { get; set; }

        public Dictionary<string, int> Products { get; set; } 
    }
}
