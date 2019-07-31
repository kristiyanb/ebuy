namespace EBuy.Tests.Services
{
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
    using EBuy.Web.Infrastructure.Mappings;
    using EBuy.Web.Models.ShoppingCart;

    public class ShoppingCartServiceTests
    {
        private readonly EBuyDbContext context;

        public ShoppingCartServiceTests()
        {
            this.context = TestStartup.GetContext();
        }

        [Fact]
        public async Task AddProductWithInvalidId()
        {
            //Arrange

            var mapper = new Mock<IMapper>().Object;
            var userService = new Mock<IUserService>().Object;
            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var shoppingCartService = new ShoppingCartService(this.context, productService, userService, mapper);

            //Act

            var result = await shoppingCartService.AddProduct("Username", "product-id", 5);

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task AddProductNew()
        {
            var user = new User() { Id = "user-id", UserName = "Username" };
            var product = new Product() { Id = "product-id", Name = "Product" };

            await this.context.AddRangeAsync(user, product);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);
            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var shoppingCartService = new ShoppingCartService(this.context, productService, userService, mapper);

            //Act

            var result = await shoppingCartService.AddProduct("Username", "product-id", 5);
            var shoppingCart = this.context.ShoppingCarts.First();
            var shoppingCartProduct = this.context.ShoppingCartProducts.First();

            //Assert

            Assert.True(result);
            Assert.Single(this.context.ShoppingCartProducts);
            Assert.Single(this.context.ShoppingCarts);
            Assert.Equal("user-id", shoppingCart.UserId);
            Assert.Equal(shoppingCart.Id, shoppingCartProduct.ShoppingCartId);
            Assert.Equal(5, shoppingCartProduct.Quantity);
        }


        [Fact]
        public async Task AddProductExisting()
        {
            var user = new User() { Id = "user-id", UserName = "Username" };
            var product = new Product() { Id = "product-id", Name = "Product" };
            var shoppingCart = new ShoppingCart() { Id = "shopping-cart-id", UserId = "user-id" };
            var shoppingCartProduct = new ShoppingCartProduct()
            {
                Name = "Product",
                Quantity = 5,
                ShoppingCartId = "shopping-cart-id"
            };

            await this.context.AddRangeAsync(user, product, shoppingCart, shoppingCartProduct);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);
            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var shoppingCartService = new ShoppingCartService(this.context, productService, userService, mapper);

            //Act

            var result = await shoppingCartService.AddProduct("Username", "product-id", 5);
            var updatedShoppingCartProduct = this.context.ShoppingCartProducts.First();

            //Assert

            Assert.True(result);
            Assert.Single(this.context.ShoppingCartProducts);
            Assert.Equal(10, updatedShoppingCartProduct.Quantity);
        }

        [Fact]
        public async Task RemoveProductWithInvalidId()
        {
            //Arrange

            var mapper = new Mock<IMapper>().Object;
            var userService = new Mock<IUserService>().Object;
            var productService = new Mock<IProductService>().Object;

            var shoppingCartService = new ShoppingCartService(this.context, productService, userService, mapper);

            //Act

            var result = await shoppingCartService.RemoveProduct("invalid-id");

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task RemoveProductWithValidId()
        {
            //Arrange

            var shoppingCartProduct = new ShoppingCartProduct() { Id = "product-id" };

            await this.context.AddAsync(shoppingCartProduct);
            await this.context.SaveChangesAsync();

            var mapper = new Mock<IMapper>().Object;
            var userService = new Mock<IUserService>().Object;
            var productService = new Mock<IProductService>().Object;

            var shoppingCartService = new ShoppingCartService(this.context, productService, userService, mapper);

            //Act

            var result = await shoppingCartService.RemoveProduct("product-id");

            //Assert

            Assert.True(result);
            Assert.Empty(this.context.ShoppingCartProducts);
        }

        [Fact]
        public async Task GetShoppingCartProductsByUsername()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username" };
            var shoppingCart = new ShoppingCart() { Id = "shopping-cart-id", UserId = "user-id" };
            var shoppingCartProduct = new ShoppingCartProduct()
            {
                Id = "product-id",
                ShoppingCartId = "shopping-cart-id"
            };

            await this.context.AddRangeAsync(user, shoppingCart, shoppingCartProduct);
            await this.context.SaveChangesAsync();

            var mapper = new Mock<IMapper>().Object;
            var userService = new Mock<IUserService>().Object;
            var productService = new Mock<IProductService>().Object;

            var shoppingCartService = new ShoppingCartService(this.context, productService, userService, mapper);

            //Act

            var products = await shoppingCartService.GetShoppingCartProductsByUsername("Username");

            //Assert

            Assert.Single(products);
        }

        [Fact]
        public async Task GetShoppingCartProductsByUsernameGeneric()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username" };
            var shoppingCart = new ShoppingCart() { Id = "shopping-cart-id", UserId = "user-id" };
            var shoppingCartProduct = new ShoppingCartProduct()
            {
                Id = "product-id",
                ShoppingCartId = "shopping-cart-id"
            };

            await this.context.AddRangeAsync(user, shoppingCart, shoppingCartProduct);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var userService = new Mock<IUserService>().Object;
            var productService = new Mock<IProductService>().Object;

            var shoppingCartService = new ShoppingCartService(this.context, productService, userService, mapper);

            //Act

            var products = await shoppingCartService
                .GetShoppingCartProductsByUsername<ShoppingCartProductViewModel>("Username");

            //Assert

            Assert.Single(products);
            Assert.IsType<ShoppingCartProductViewModel>(products[0]);
        }
    }
}