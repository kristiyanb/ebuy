namespace EBuy.Tests.Services
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;
    using Xunit;

    using EBuy.Data;
    using EBuy.Services;
    using EBuy.Models;
    using EBuy.Web.Areas.Admin.Models.Purchases;
    using EBuy.Web.Infrastructure.Mappings;
    using EBuy.Web.Areas.Identity.Pages.Account.Manage;

    public class PurchaseServiceTests
    {
        private readonly EBuyDbContext context;

        public PurchaseServiceTests()
        {
            this.context = TestStartup.GetContext();
        }

        [Fact]
        public async Task GetAll()
        {
            //Arrange

            var firstUser = new User() { Id = "first-user-id", UserName = "FirstUsername" };
            var secondUser = new User() { Id = "second-user-id", UserName = "SecondUsername" };
            var firstPurchase = new Purchase() { Id = "first-purchase-id", DateOfOrder = DateTime.MinValue, UserId = "first-user-id" };
            var secondPurchase = new Purchase() { DateOfOrder = DateTime.MaxValue, UserId = "second-user-id" };
            var product = new PurchasedProduct() { Id = "purchased-product-id", PurchaseId = "first-purchase-id" };

            await this.context.AddRangeAsync(firstUser, secondUser, firstPurchase, secondPurchase, product);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var purchaseService = new PurchaseService(this.context, mapper);

            //Act

            var purchases = await purchaseService.GetAll<PurchaseViewModel>();

            //Assert

            Assert.Equal("SecondUsername", purchases[0].Username);
            Assert.Equal("FirstUsername", purchases[1].Username);
            Assert.NotNull(purchases[1].Products[0]);
        }

        [Fact]
        public async Task GetUserPurchaseHistory()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username" };
            var firstPurchase = new Purchase() { Id = "first-purchase-id", UserId = "user-id" };
            var secondPurchase = new Purchase() { DateOfOrder = DateTime.MaxValue };
            var product = new PurchasedProduct() { Id = "purchased-product-id", PurchaseId = "first-purchase-id" };

            await this.context.AddRangeAsync(user, firstPurchase, secondPurchase, product);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();


            var purchaseService = new PurchaseService(this.context, mapper);

            //Act

            var purchases = await purchaseService
                .GetUserPurchaseHistory<PurchaseHistoryModel.PurchaseViewModel>("Username");

            //Assert

            Assert.Single(purchases);
            Assert.NotNull(purchases[0].Products[0]);
        }
    }
}
