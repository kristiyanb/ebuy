namespace EBuy.Data
{
    using System;
    using EBuy.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class EBuyDbContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<PurchasedProduct> PurchasedProducts { get; set; }

        public EBuyDbContext(DbContextOptions<EBuyDbContext> options)
            : base(options)
        {
        }

        public object FirstOrDefaultAsync(string commentId)
        {
            throw new NotImplementedException();
        }
    }
}
