namespace EBuy.Web.Models.ShoppingCart
{
    using EBuy.Models;
    using EBuy.Services.Mapping;

    public class ShoppingCartProductViewModel : IMapFrom<ShoppingCartProduct>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
