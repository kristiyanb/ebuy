namespace EBuy.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Xunit;

    using EBuy.Data;
    using EBuy.Models;
    using EBuy.Services;
    using EBuy.Services.Contracts;
    using EBuy.Services.Models;
    using EBuy.Web.Infrastructure.Mappings;

    public class OrderServiceTests
    {
        private readonly EBuyDbContext context;

        public OrderServiceTests()
        {
            this.context = TestStartup.GetContext();
        }

        [Fact]
        public async Task CreateInvalidProductsFiltering()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username" };
            var dbProduct = new Product()
            {
                Id = "product-id",
                Name = "Product",
                InStock = 10,
                PurchasesCount = 10,
                Price = 10,
                ImageUrl = "ImageUrl"
            };

            var secondProduct = new Product() { Id = "second-product-id" };

            await this.context.AddRangeAsync(user, dbProduct, secondProduct);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);
            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var orderService = new OrderService(this.context, mapper, userService, productService);

            var orderDto = new OrderDto()
            {
                Address = "Address",
                Email = "Email",
                FullName = "FullName",
                PaymentMethod = "PaymentMethod",
                PhoneNumber = "PhoneNumber",
                Products = new Dictionary<string, int>()
            };

            orderDto.Products.Add("product-id", 1);
            orderDto.Products.Add("second-product-id", 0);
            orderDto.Products.Add("invalid-id", 1);

            //Act

            var result = await orderService.Create(orderDto, "Username");

            //Assert

            Assert.True(result);
            Assert.Single(this.context.Purchases);
            Assert.Single(this.context.PurchasedProducts);
        }

        [Fact]
        public async Task CreateMapping()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username" };
            var dbProduct = new Product()
            {
                Id = "product-id",
                Name = "Product",
                InStock = 10,
                PurchasesCount = 10,
                Price = 10,
                ImageUrl = "ImageUrl"
            };

            await this.context.AddRangeAsync(user, dbProduct);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);
            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var orderService = new OrderService(this.context, mapper, userService, productService);

            var orderDto = new OrderDto()
            {
                Address = "Address",
                Email = "Email",
                FullName = "FullName",
                PaymentMethod = "PaymentMethod",
                PhoneNumber = "PhoneNumber",
                Products = new Dictionary<string, int>()
            };

            orderDto.Products.Add("product-id", 1);

            //Act

            await orderService.Create(orderDto, "Username");

            //Assert

            var purchase = this.context.Purchases.First();
            var purchasedProduct = this.context.PurchasedProducts.First();

            Assert.Equal("Address", purchase.Address);
            Assert.Equal("Email", purchase.Email);
            Assert.Equal("FullName", purchase.FullName);
            Assert.Equal("PhoneNumber", purchase.PhoneNumber);
            Assert.Equal("user-id", purchase.UserId);

            Assert.Equal(purchase, purchasedProduct.Purchase);
            Assert.Equal("Product", purchasedProduct.Name);
            Assert.Equal("ImageUrl", purchasedProduct.ImageUrl);
            Assert.Equal(10, purchasedProduct.Price);
        }

        [Fact]
        public async Task CreateQuantityAndSalesUpdate()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username" };
            var dbProduct = new Product()
            {
                Id = "product-id",
                Name = "Product",
                InStock = 10,
                PurchasesCount = 10,
                Price = 10,
                ImageUrl = "ImageUrl"
            };

            await this.context.AddRangeAsync(user, dbProduct);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);
            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var orderService = new OrderService(this.context, mapper, userService, productService);

            var orderDto = new OrderDto()
            {
                Address = "Address",
                Email = "Email",
                FullName = "FullName",
                PaymentMethod = "PaymentMethod",
                PhoneNumber = "PhoneNumber",
                Products = new Dictionary<string, int>()
            };

            orderDto.Products.Add("product-id", 1);

            //Act

            await orderService.Create(orderDto, "Username");

            //Assert

            var product = this.context.Products.First();

            Assert.Equal(9, product.InStock);
            Assert.Equal(11, product.PurchasesCount);
        }

        [Fact]
        public async Task CreateWithNoValidData()
        {

            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username" };

            await this.context.AddAsync(user);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);
            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var orderService = new OrderService(this.context, mapper, userService, productService);

            var orderDto = new OrderDto()
            {
                Address = "Address",
                Email = "Email",
                FullName = "FullName",
                PaymentMethod = "PaymentMethod",
                PhoneNumber = "PhoneNumber",
                Products = new Dictionary<string, int>()
            };

            orderDto.Products.Add("invalid-id", 0);

            //Act

            var result = await orderService.Create(orderDto, "Username");

            //Assert

            Assert.False(result);
            Assert.Empty(this.context.Purchases);
            Assert.Empty(this.context.PurchasedProducts);
        }

        [Fact]
        public async Task CreateWithInvalidUsername()
        {
            //Arrange

            var dbProduct = new Product()
            {
                Id = "product-id",
                Name = "Product",
                InStock = 10,
                PurchasesCount = 10,
                Price = 10,
                ImageUrl = "ImageUrl"
            };

            await this.context.AddAsync(dbProduct);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);
            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var orderService = new OrderService(this.context, mapper, userService, productService);

            var orderDto = new OrderDto()
            {
                Address = "Address",
                Email = "Email",
                FullName = "FullName",
                PaymentMethod = "PaymentMethod",
                PhoneNumber = "PhoneNumber",
                Products = new Dictionary<string, int>()
            };

            orderDto.Products.Add("product-id", 1);
            orderDto.Products.Add("invalid-id", 0);

            //Act

            var result = await orderService.Create(orderDto, "");

            //Assert

            Assert.True(result);
            Assert.Single(this.context.Purchases);
            Assert.Single(this.context.PurchasedProducts);

            var purchase = this.context.Purchases.First();
            var purchasedProduct = this.context.PurchasedProducts.First();
            var product = this.context.Products.First();

            Assert.Equal("Address", purchase.Address);
            Assert.Equal("Email", purchase.Email);
            Assert.Equal("FullName", purchase.FullName);
            Assert.Equal("PhoneNumber", purchase.PhoneNumber);
            Assert.Null(purchase.UserId);

            Assert.Equal(purchase, purchasedProduct.Purchase);
            Assert.Equal("Product", purchasedProduct.Name);
            Assert.Equal("ImageUrl", purchasedProduct.ImageUrl);
            Assert.Equal(10, purchasedProduct.Price);
            Assert.Equal(9, product.InStock);
            Assert.Equal(11, product.PurchasesCount);
        }
    }
}
