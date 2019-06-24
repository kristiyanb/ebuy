﻿namespace EBuy.Models
{
    using System;

    public class Comment
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime LastModified { get; set; }
    }
}
