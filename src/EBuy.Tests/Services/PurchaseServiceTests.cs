namespace EBuy.Tests.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Xunit;

    using EBuy.Data;
    using EBuy.Services;
    using EBuy.Services.Contracts;
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
        public async Task AddWithValidUser()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username" };

            await this.context.AddAsync(user);
            await this.context.SaveChangesAsync();

            var mapper = new Mock<IMapper>().Object;
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);
            var productService = new Mock<IProductService>().Object;

            var purchaseService = new PurchaseService(this.context, userService, productService, mapper);

            //Act

            var purchase = await purchaseService.Add("address", "Username");

            //Assert

            Assert.Single(this.context.Purchases);
            Assert.Equal("Username", purchase.User.UserName);
            Assert.Equal("address", purchase.Address);
        }

        [Fact]
        public async Task AddWithInvalidUser()
        {
            //Arrange

            var mapper = new Mock<IMapper>().Object;
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);
            var productService = new Mock<IProductService>().Object;

            var purchaseService = new PurchaseService(this.context, userService, productService, mapper);

            //Act

            var purchase = await purchaseService.Add("address", "");

            //Assert

            Assert.Single(this.context.Purchases);
            Assert.Equal("address", purchase.Address);
        }

        [Fact]
        public async Task AddPurchasedProduct()
        {
            //Arrange

            var purchase = new Purchase() { Id = "purchase-id" };
            var shoppingCartProduct = new ShoppingCartProduct() { Id = "product-id", Name = "Product" };

            await this.context.AddRangeAsync(purchase, shoppingCartProduct);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);
            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var purchaseService = new PurchaseService(this.context, userService, productService, mapper);

            //Act

            await purchaseService.AddPurchasedProduct(shoppingCartProduct, purchase);
            await this.context.SaveChangesAsync();

            var purchasedProduct = this.context.PurchasedProducts.FirstOrDefault();

            //Assert

            Assert.Single(this.context.PurchasedProducts);
            Assert.NotNull(purchasedProduct);
            Assert.Equal("purchase-id", purchasedProduct.PurchaseId);
            Assert.Equal("Product", purchasedProduct.Name);
        }

        [Fact]
        public async Task GetAll()
        {
            //Arrange

            var firstUser = new User() { Id = "first-user-id", Email = "FirstEmail" };
            var secondUser = new User() { Id = "second-user-id", Email = "SecondEmail" };
            var firstPurchase = new Purchase() { Id = "first-purchase-id", DateOfOrder = DateTime.MinValue, UserId = "first-user-id" };
            var secondPurchase = new Purchase() { DateOfOrder = DateTime.MaxValue, UserId = "second-user-id" };
            var product = new PurchasedProduct() { Id = "purchased-product-id", PurchaseId = "first-purchase-id" };

            await this.context.AddRangeAsync(firstUser, secondUser, firstPurchase, secondPurchase, product);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var userService = new Mock<IUserService>().Object;
            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var productService = new Mock<IProductService>().Object;

            var purchaseService = new PurchaseService(this.context, userService, productService, mapper);

            //Act

            var purchases = await purchaseService.GetAll<PurchaseViewModel>();

            //Assert

            Assert.Equal("SecondEmail", purchases[0].UserEmail);
            Assert.Equal("FirstEmail", purchases[1].UserEmail);
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
            var userService = new Mock<IUserService>().Object;
            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var productService = new Mock<IProductService>().Object;

            var purchaseService = new PurchaseService(this.context, userService, productService, mapper);

            //Act

            var purchases = await purchaseService
                .GetUserPurchaseHistory<PurchaseHistoryModel.PurchaseViewModel>("Username");

            //Assert

            Assert.Single(purchases);
            Assert.NotNull(purchases[0].Products[0]);
        }
    }
}
