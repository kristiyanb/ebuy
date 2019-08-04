namespace EBuy.Tests
{
    using System;

    using Microsoft.EntityFrameworkCore;

    using EBuy.Data;

    public class TestStartup
    {
        public static EBuyDbContext GetContext()
        {
            var dbOptions = new DbContextOptionsBuilder<EBuyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new EBuyDbContext(dbOptions);

            return context;
        }
    }
}
