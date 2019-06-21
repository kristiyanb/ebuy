namespace EBuy.Data
{
    using EBuy.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class EBuyDbContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Purchase> Purchases { get; set; }


        public EBuyDbContext(DbContextOptions<EBuyDbContext> options)
            : base(options)
        {
        }
    }
}
