using System.ComponentModel.DataAnnotations;

namespace EBuy.Web.Models.ShoppingCart
{
    public class ShoppingCartProductInputModel
    {
        public string Id { get; set; }

        [Range(1, 50000)]
        public int Quantity { get; set; }
    }
}
