namespace EBuy.Web.Models.Products
{
    using EBuy.Models;
    using EBuy.Services.Mapping;

    public class ProductGridModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public double Rating { get; set; }

        public string ImageUrl { get; set; }
    }
}
