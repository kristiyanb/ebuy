namespace EBuy.Tests.Services
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Xunit;

    using EBuy.Data;

    public class ShoppingCartServiceTests
    {
        private readonly EBuyDbContext context;

        public ShoppingCartServiceTests()
        {
            this.context = TestStartup.GetContext();
        }
    }
}