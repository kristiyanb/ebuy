namespace EBuy.Tests.Services
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Xunit;

    using EBuy.Data;

    public class ProductServiceTests
    {
        private readonly EBuyDbContext context;

        public ProductServiceTests()
        {
            this.context = TestStartup.GetContext();
        }
    }
}
