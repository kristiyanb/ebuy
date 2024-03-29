﻿namespace EBuy.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using EBuy.Models;

    public class EBuyDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<PurchasedProduct> PurchasedProducts { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public EBuyDbContext(DbContextOptions<EBuyDbContext> options)
            : base(options)
        {
        }
    }
}
