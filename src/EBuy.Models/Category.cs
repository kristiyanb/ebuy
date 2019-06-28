namespace EBuy.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
