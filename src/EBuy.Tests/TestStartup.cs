namespace EBuy.Tests
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using EBuy.Data;
    using EBuy.Web;

    public class TestStartup
    {
        public static EBuyDbContext GetContext()
        {
            var dbOptions = new DbContextOptionsBuilder<EBuyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new EBuyDbContext(dbOptions);
        }
    }
}
