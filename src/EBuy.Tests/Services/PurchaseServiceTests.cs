namespace EBuy.Tests.Services
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Xunit;

    using EBuy.Data;

    public class PurchaseServiceTests
    {
        private readonly EBuyDbContext context;

        public PurchaseServiceTests()
        {
            this.context = TestStartup.GetContext();
        }
    }
}
