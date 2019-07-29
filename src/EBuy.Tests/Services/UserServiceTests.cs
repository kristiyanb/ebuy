namespace EBuy.Tests.Services
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Xunit;

    using EBuy.Data;

    public class UserServiceTests
    {
        private readonly EBuyDbContext context;

        public UserServiceTests()
        {
            this.context = TestStartup.GetContext();
        }
    }
}
