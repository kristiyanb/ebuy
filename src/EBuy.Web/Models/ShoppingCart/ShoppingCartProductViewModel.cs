namespace EBuy.Web.Models.ShoppingCart
{
    public class ShoppingCartProductViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
