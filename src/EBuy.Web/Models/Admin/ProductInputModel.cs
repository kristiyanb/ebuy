namespace EBuy.Web.Models.Admin
{
    public class ProductInputModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int InStock { get; set; }

        public string CategoryName { get; set; }
    }
}
