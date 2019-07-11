namespace EBuy.Web.Models.ShoppingCart
{
    public class ShoppingCartProductInputModel
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
