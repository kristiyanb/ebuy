namespace EBuy.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Content { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime LastModified { get; set; }
    }
}
