namespace EBuy.Web.Models.Products
{
    public class ProductDetailsModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public double Rating { get; set; }

        public int InStock { get; set; }

        public string ImageUrl { get; set; }

        public int Purchases { get; set; }
    }
}
