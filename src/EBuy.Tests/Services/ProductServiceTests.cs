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
    using EBuy.Services.Models;
    using EBuy.Models;
    using EBuy.Web.Areas.Admin.Models.Products;
    using EBuy.Web.Infrastructure.Mappings;
    using EBuy.Web.Models.Products;

    public class ProductServiceTests
    {
        private readonly EBuyDbContext context;

        public ProductServiceTests()
        {
            this.context = TestStartup.GetContext();
        }

        [Fact]
        public async Task GetProductById()
        {
            //Arrage

            var dbProduct = new Product() { Id = "product-id", Name = "Product" };

            await this.context.AddAsync(dbProduct);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapper = new Mock<IMapper>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var product = await productService.GetProductById("product-id");

            //Assert

            Assert.NotNull(product);
            Assert.IsType<Product>(product);
            Assert.Equal("Product", product.Name);
        }

        [Fact]
        public async Task GetProductByIdToGenericViewModel()
        {
            //Arrage

            var dbProduct = new Product() { Id = "product-id", Name = "Product" };

            await this.context.AddAsync(dbProduct);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var product = await productService.GetProductById<ProductDetailsModel>("product-id");

            //Assert

            Assert.NotNull(product);
            Assert.IsType<ProductDetailsModel>(product);
            Assert.Equal("Product", product.Name);
        }

        [Fact]
        public async Task GetProductByIdToGenericViewModelRatingMapping()
        {
            //Arrage

            var dbProduct = new Product() { Id = "product-id", Name = "Product", VotesCount = 2, Score = 7 };

            await this.context.AddAsync(dbProduct);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var product = await productService.GetProductById<ProductDetailsModel>("product-id");

            //Assert

            Assert.Equal(3.5, product.Rating);
        }

        [Fact]
        public async Task GetProductByIdToGenericViewModelZeroRatingMapping()
        {
            //Arrage

            var dbProduct = new Product() { Id = "product-id", Name = "Product", VotesCount = 0, Score = 0 };

            await this.context.AddAsync(dbProduct);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var product = await productService.GetProductById<ProductDetailsModel>("product-id");

            //Assert

            Assert.Equal(0.0, product.Rating);
        }

        [Fact]
        public async Task GetProductByIdToGenericViewModelCommentsMapping()
        {
            //Arrage

            var dbProduct = new Product() { Id = "product-id", Name = "Product", VotesCount = 2, Score = 7 };
            var comment = new Comment() { Id = "comment-id", ProductId = "product-id" };

            this.context.AddRange(dbProduct, comment);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var product = await productService.GetProductById<ProductEditModel>("product-id");

            //Assert

            Assert.Single(product.Comments);
            Assert.Equal("comment-id", product.Comments[0].Id);
        }

        [Fact]
        public async Task GetProductsByNameOrCategoryMatchWithValidProductNameArgument()
        {
            //Arrage

            var product = new Product() { Id = "product-id", Name = "Product" };

            await this.context.AddAsync(product);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var matches = await productService.GetProductsByNameOrCategoryMatch<ProductGridModel>("Product");

            //Assert

            Assert.Single(matches);
        }

        [Fact]
        public async Task GetProductsByNameOrCategoryMatchWithValidCategoryNameArgument()
        {
            //Arrage

            var product = new Product() { Id = "product-id", Name = "Product", CategoryId = "category-id" };

            var category = new Category() { Id = "category-id", Name = "Category" };

            this.context.AddRange(product, category);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var matches = await productService.GetProductsByNameOrCategoryMatch<ProductGridModel>("Category");

            //Assert

            Assert.Single(matches);
        }

        [Fact]
        public async Task GetProductsByNameOrCategoryMatchWithDeletedProductNameArgument()
        {
            //Arrage

            var product = new Product() { Id = "product-id", Name = "Product", IsDeleted = true };

            var category = new Category() { Id = "category-id", Name = "Category" };

            this.context.AddRange(product, category);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var matches = await productService.GetProductsByNameOrCategoryMatch<ProductGridModel>("Product");

            //Assert

            Assert.Empty(matches);
        }

        [Fact]
        public async Task GetProductByNameOrCategoryMatchViewModelRatingMapping()
        {
            //Arrage

            var product = new Product() { Id = "product-id", Name = "Product", Score = 10, VotesCount = 2 };

            await this.context.AddAsync(product);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var matches = await productService.GetProductsByNameOrCategoryMatch<ProductGridModel>("Product");

            //Assert

            Assert.Equal(5, matches[0].Rating);
        }

        [Fact]
        public async Task GetProductByNameOrCategoryMatchViewModelZeroRatingMapping()
        {
            //Arrage

            var product = new Product() { Id = "product-id", Name = "Product", Score = 0, VotesCount = 0 };

            await this.context.AddAsync(product);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var matches = await productService.GetProductsByNameOrCategoryMatch<ProductGridModel>("Product");

            //Assert

            Assert.Equal(0.0, matches[0].Rating);
        }

        [Fact]
        public async Task GetLastFiveProducts()
        {
            //Arrange

            var firstProduct = new Product() { Id = "first-product-id", Name = "FirstProduct" };
            var secondProduct = new Product() { Id = "second-product-id", Name = "SecondProduct" };
            var thirdProduct = new Product() { Id = "third-product-id", Name = "ThirdProduct" };
            var fourthProduct = new Product() { Id = "fourth-product-id", Name = "FourthProduct", IsDeleted = true };
            var fifthProduct = new Product() { Id = "fifth-product-id", Name = "FifthProduct" };
            var sixthProduct = new Product() { Id = "sixth-product-id", Name = "SixthProduct" };

            this.context.AddRange(firstProduct, secondProduct, thirdProduct, fourthProduct, fifthProduct, sixthProduct);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var products = await productService.GetLastFiveProducts<ProductGridModel>();

            //Assert

            Assert.Equal(5, products.Count);
            Assert.False(products.FirstOrDefault(x => x.Id == "fourth-product-id") != null);
        }

        [Fact]
        public async Task GetAllWithoutCategoryArgument()
        {
            //Arrange

            var firstProduct = new Product() { Id = "first-product-id", Name = "FirstProduct" };
            var secondProduct = new Product() { Id = "second-product-id", Name = "SecondProduct", IsDeleted = true };

            this.context.AddRange(firstProduct, secondProduct);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var products = await productService.GetAll<ProductDataModel>(string.Empty);

            //Assert

            Assert.Single(products);
            Assert.Equal("first-product-id", products[0].Id);
        }

        [Fact]
        public async Task GetAllWithCategoryArgument()
        {
            //Arrange

            var firstProduct = new Product() { Id = "first-product-id", Name = "FirstProduct", CategoryId = "category-id" };
            var secondProduct = new Product() { Id = "second-product-id", Name = "Category" };

            var category = new Category() { Id = "category-id", Name = "Category" };

            this.context.AddRange(firstProduct, secondProduct, category);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var products = await productService.GetAll<ProductDataModel>("Category");

            //Assert

            Assert.Single(products);
            Assert.Equal("first-product-id", products[0].Id);
        }

        [Fact]
        public async Task GetAllRatingMapping()
        {
            //Arrange

            var product = new Product() { Id = "product-id", Name = "Product", Score = 18, VotesCount = 6 };

            await this.context.AddAsync(product);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var products = await productService.GetAll<ProductDataModel>(string.Empty);

            //Assert

            Assert.Equal(3, products[0].Rating);
        }

        [Fact]
        public async Task GetAllZeroRatingMapping()
        {
            //Arrange

            var product = new Product() { Id = "product-id", Name = "Product", Score = 0, VotesCount = 0 };

            await this.context.AddAsync(product);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var products = await productService.GetAll<ProductDataModel>(string.Empty);

            //Assert

            Assert.Equal(0.0, products[0].Rating);
        }

        [Fact]
        public async Task GetDeleted()
        {
            //Arrange

            var firstProduct = new Product() { Id = "first-product-id", Name = "FirstProduct" };
            var secondProduct = new Product() { Id = "second-product-id", Name = "SecondProduct", IsDeleted = true };

            this.context.AddRange(firstProduct, secondProduct);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var products = await productService.GetDeleted<ProductDataModel>();

            //Assert

            Assert.Single(products);
            Assert.Equal("second-product-id", products[0].Id);
        }

        [Fact]
        public async Task AddWithValidCategory()
        {
            //Arrange

            var category = new Category() { Id = "category-id", Name = "Category" };

            await this.context.AddAsync(category);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new CategoryService(this.context, cloudinaryService, mapper);
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var productDto = new ProductDto() { Name = "Product", CategoryName = "Category" };

            //Act

            var result = await productService.Add(productDto);
            var product = this.context.Products.First();

            //Assert

            Assert.Single(this.context.Products);
            Assert.Equal("Product", product.Name);
            Assert.Equal("Category", product.Category.Name);
            Assert.True(result);
        }

        [Fact]
        public async Task AddWithInvalidCategory()
        {
            //Arrange

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new CategoryService(this.context, cloudinaryService, mapper);
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var productDto = new ProductDto() { Name = "Product", CategoryName = "InvalidCategory" };

            //Act

            var result = await productService.Add(productDto);

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task EditWithInvalidProductIdAndCategoryName()
        {
            //Arrange

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new CategoryService(this.context, cloudinaryService, mapper);
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var productDto = new ProductDto() { Id = "invalid-id", CategoryName = "InvalidCategory" };

            //Act

            var result = await productService.Edit(productDto);

            //Assert

            Assert.False(result);
        }


        [Fact]
        public async Task EditWithValidProductIdAndInvalidCategoryName()
        {
            //Arrange

            var product = new Product() { Id = "product-id" };

            await this.context.AddAsync(product);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new CategoryService(this.context, cloudinaryService, mapper);
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var productDto = new ProductDto() { Id = "product-id", CategoryName = "InvalidCategory" };

            //Act

            var result = await productService.Edit(productDto);

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task EditWithInvalidProductIdAndValidCategoryName()
        {
            //Arrange

            var category = new Category() { Name = "Category" };

            await this.context.AddAsync(category);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new CategoryService(this.context, cloudinaryService, mapper);
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var productDto = new ProductDto() { Id = "invalid-id", CategoryName = "Category" };

            //Act

            var result = await productService.Edit(productDto);

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task EditWithValidProductIdAndCategoryName()
        {
            //Arrange

            var category = new Category() { Id = "category-id", Name = "Category" };
            var product = new Product() { Id = "product-id", Name = "Product" };

            await this.context.AddRangeAsync(category, product);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var categoryService = new CategoryService(this.context, cloudinaryService, mapper);
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            var productDto = new ProductDto()
            {
                Id = "product-id",
                CategoryName = "Category",
                Name = "UpdatedProduct"
            };

            //Act

            var result = await productService.Edit(productDto);
            var updatedProduct = this.context.Products.First();

            //Assert

            Assert.True(result);
            Assert.Equal("UpdatedProduct", updatedProduct.Name);
            Assert.Equal("category-id", updatedProduct.CategoryId);
            Assert.Single(this.context.Products);
        }

        [Fact]
        public async Task RemoveWithInvalidId()
        {
            //Arrange

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapper = new Mock<IMapper>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var result = await productService.Remove("invalid-id");

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task RemoveWithValidId()
        {
            //Arrange

            var product = new Product() { Id = "product-id" };

            await this.context.AddAsync(product);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapper = new Mock<IMapper>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var result = await productService.Remove("product-id");
            var deletedProduct = this.context.Products.First();

            //Assert

            Assert.True(result);
            Assert.True(deletedProduct.IsDeleted);
        }

        [Fact]
        public async Task RestoreWithInvalidId()
        {
            //Arrange

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapper = new Mock<IMapper>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var result = await productService.Restore("invalid-id");

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task RestoreWithValidId()
        {
            //Arrange

            var product = new Product() { Id = "product-id", IsDeleted = true };

            await this.context.AddAsync(product);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapper = new Mock<IMapper>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var result = await productService.Restore("product-id");
            var deletedProduct = this.context.Products.First();

            //Assert

            Assert.True(result);
            Assert.False(deletedProduct.IsDeleted);
        }

        [Fact]
        public async Task UpdateRatingWithInvalidInput()
        {
            //Arrange

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapper = new Mock<IMapper>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var result = await productService.UpdateRating("Username", "product-id", "75");

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateRatingWithValidInputNewVote()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username" };
            var product = new Product() { Id = "product-id", Name = "Product" };

            await this.context.AddRangeAsync(user, product);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapper = new Mock<IMapper>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var result = await productService.UpdateRating("Username", "product-id", "5");
            var updatedProduct = this.context.Products.First();
            var vote = this.context.Votes.FirstOrDefault();

            //Assert

            Assert.True(result);
            Assert.Equal(5, updatedProduct.Score);
            Assert.Equal(1, updatedProduct.VotesCount);
            Assert.Single(this.context.Votes);
            Assert.Equal("product-id", vote.ProductId);
            Assert.Equal("user-id", vote.UserId);
            Assert.Equal(5, vote.Score);
        }


        [Fact]
        public async Task UpdateRatingWithValidInputExistingVote()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username" };
            var product = new Product() { Id = "product-id", Name = "Product", Score = 3, VotesCount = 1 };
            var vote = new Vote() { ProductId = "product-id", UserId = "user-id", Score = 3 };

            await this.context.AddRangeAsync(user, product, vote);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapper = new Mock<IMapper>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var result = await productService.UpdateRating("Username", "product-id", "5");
            var updatedProduct = this.context.Products.First();
            var updatedVote = this.context.Votes.FirstOrDefault();

            //Assert

            Assert.True(result);
            Assert.Equal(5, updatedProduct.Score);
            Assert.Equal(1, updatedProduct.VotesCount);
            Assert.Single(this.context.Votes);
            Assert.Equal("product-id", updatedVote.ProductId);
            Assert.Equal("user-id", updatedVote.UserId);
            Assert.Equal(5, updatedVote.Score);
        }


        [Fact]
        public async Task UpdateRatingWithValidInputExistingVoteSameScore()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username" };
            var product = new Product() { Id = "product-id", Name = "Product", Score = 5, VotesCount = 1 };
            var vote = new Vote() { ProductId = "product-id", UserId = "user-id", Score = 5 };

            await this.context.AddRangeAsync(user, product, vote);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapper = new Mock<IMapper>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var result = await productService.UpdateRating("Username", "product-id", "5");

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateProductQuantityAndSalesWithInvalidArguments()
        {
            //Arrange

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapper = new Mock<IMapper>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var result = await productService.UpdateProductQuantityAndSales("InvalidName", "", 0, 0);

            //Assert

            Assert.False(result);
        }


        [Fact]
        public async Task UpdateProductQuantityAndSalesWithValidArguments()
        {
            //Arrange

            var product = new Product() { Name = "Product", ImageUrl = "ImageUrl", Price = 10, InStock = 10 };

            await this.context.AddAsync(product);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>().Object;
            var mapper = new Mock<IMapper>().Object;
            var categoryService = new Mock<ICategoryService>().Object;
            var userService = new Mock<IUserService>().Object;

            var productService = new ProductService(this.context, cloudinaryService, mapper, categoryService, userService);

            //Act

            var result = await productService.UpdateProductQuantityAndSales("Product", "ImageUrl", 10, 1);
            var updatedProduct = this.context.Products.First();

            //Assert

            Assert.True(result);
            Assert.Equal(1, updatedProduct.PurchasesCount);
            Assert.Equal(9, updatedProduct.InStock);
        }
    }
}