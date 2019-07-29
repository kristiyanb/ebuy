namespace EBuy.Tests.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Moq;
    using Xunit;

    using EBuy.Data;
    using EBuy.Models;
    using EBuy.Services;
    using EBuy.Services.Contracts;
    using EBuy.Web.Models.Categories;
    using EBuy.Services.Models;
    using EBuy.Web.Infrastructure.Mappings;

    public class CategoryServiceTests
    {
        private readonly EBuyDbContext context;

        public CategoryServiceTests()
        {
            this.context = TestStartup.GetContext();
        }

        [Fact]
        public async Task GetCategoryNames()
        {
            //Arrange

            var firstCategory = new Category() { Name = "1" };
            var secondCategory = new Category() { Name = "2" };

            this.context.AddRange(firstCategory, secondCategory);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>();
            var mapper = new Mock<IMapper>();

            var categoryService = new CategoryService(this.context, cloudinaryService.Object, mapper.Object);

            //Act

            var categoryNames = await categoryService.GetCategoryNames();

            //Assert

            Assert.Equal("1", categoryNames.ToList()[0]);
            Assert.Equal("2", categoryNames.ToList()[1]);
            Assert.Equal(2, categoryNames.Count());
        }

        [Fact]
        public async Task GetCategories()
        {
            //Arrange

            var firstCategory = new Category() { Id = "1", ImageUrl = "1", Name = "1" };
            var secondCategory = new Category() { Id = "2", ImageUrl = "2", Name = "2" };
            var firstProduct = new Product() { CategoryId = "1" };
            var secondProduct = new Product() { CategoryId = "1" };

            this.context.AddRange(firstCategory, secondCategory, firstProduct, secondProduct);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>();
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var categoryService = new CategoryService(this.context, cloudinaryService.Object, mapper);

            //Act

            var categories = await categoryService.GetCategories<CategoryGridModel>();

            var categoryList = categories.ToList();

            //Assert

            Assert.IsType<CategoryGridModel>(categoryList[0]);
            Assert.Equal("1", categoryList[0].Name);
            Assert.Equal("1", categoryList[0].ImageUrl);
            Assert.Equal(2, categoryList[0].ProductsCount);
            Assert.Equal(2, categoryList.Count);
        }

        [Fact]
        public async Task AddCategory()
        {
            //Arrange

            var cloudinaryService = new Mock<ICloudinaryService>();
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var categoryService = new CategoryService(this.context, cloudinaryService.Object, mapper);

            //Act

            var categorydto = new CategoryDto
            {
                Id = "1",
                Image = null,
                Name = "1"
            };

            await categoryService.Add(categorydto);

            //Assert

            Assert.Equal(1, this.context.Categories.Count());
        }

        [Fact]
        public async Task GetCategoryByName()
        {
            //Arrange

            var firstCategory = new Category() { Id = "1", ImageUrl = "1", Name = "1" };
            var secondCategory = new Category() { Id = "2", ImageUrl = "2", Name = "2" };

            this.context.AddRange(firstCategory, secondCategory);
            await this.context.SaveChangesAsync();

            var cloudinaryService = new Mock<ICloudinaryService>();
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var categoryService = new CategoryService(this.context, cloudinaryService.Object, mapper);

            //Act

            var category = await categoryService.GetCategoryByName("2");

            //Assert

            Assert.Equal("2", category.Id);
            Assert.Equal("2", category.Name);
        }
    }
}
