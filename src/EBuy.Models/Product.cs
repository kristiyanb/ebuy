namespace EBuy.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public Product()
        {
            this.Comments = new HashSet<Comment>();
        }
        
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public int VotesCount { get; set; }

        public int PurchasesCount { get; set; }

        public double Score { get; set; }

        public int InStock { get; set; }

        public bool IsDeleted { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
