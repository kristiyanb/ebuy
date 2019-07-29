namespace EBuy.Tests.Services
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Xunit;

    using EBuy.Data;

    public class CheckoutServiceTests
    {
        private readonly EBuyDbContext context;

        public CheckoutServiceTests()
        {
            this.context = TestStartup.GetContext();
        }
    }
}
