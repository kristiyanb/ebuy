namespace EBuy.Web.Models.ShoppingCart
{
    using System.ComponentModel.DataAnnotations;

    public class ShoppingCartProductInputModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [Range(1, 5000)]
        public int Quantity { get; set; }
    }
}
