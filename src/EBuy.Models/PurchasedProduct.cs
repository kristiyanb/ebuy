namespace EBuy.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PurchasedProduct
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public string PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
    }
}
