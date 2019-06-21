namespace EBuy.Models
{
    using System.Collections.Generic;

    public class Product
    {
        public Product()
        {
            this.Comments = new HashSet<Comment>();
        }
        
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public int PurchasesCount { get; set; }

        public double Score { get; set; }

        public int InStock { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
