namespace EBuy.Web.Models.Products
{
    using EBuy.Models;
    using EBuy.Services.Mapping;
    using System.Collections.Generic;

    public class ProductDetailsModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public double Rating { get; set; }

        public int InStock { get; set; }

        public string ImageUrl { get; set; }

        public int Purchases { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
