namespace EBuy.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ShoppingCartProduct
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
