namespace EBuy.Web.Models.ShoppingCart
{
    using System.ComponentModel.DataAnnotations;

    public class ShoppingCartProductInputModel
    {
        public string Id { get; set; }

        [Range(1, 50000)]
        public int Quantity { get; set; }
    }
}
