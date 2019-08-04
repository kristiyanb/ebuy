namespace EBuy.Web.Models.Orders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderInputModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(40, MinimumLength = 4)]
        public string Email { get; set; }

        [Required]
        [StringLength(17, MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 10)]
        public string Address { get; set; }

        public string PaymentMethod { get; set; }

        public Dictionary<string, int> Products { get; set; }
    }
}
