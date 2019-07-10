namespace EBuy.Models
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<ShoppingCartProduct> Products { get; set; }
    }
}
