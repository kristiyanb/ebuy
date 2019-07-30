namespace EBuy.Tests.Services
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Xunit;

    using EBuy.Data;
    using EBuy.Services;
    using EBuy.Services.Contracts;
    using EBuy.Web.Infrastructure.Mappings;
    using EBuy.Web.Models.Comments;
    using Models;

    public class CommentServiceTests
    {
        private readonly EBuyDbContext context;

        public CommentServiceTests()
        {
            this.context = TestStartup.GetContext();
        }

        [Fact]
        public async Task GetCommentsByProductId()
        {
            //Arrange

            var firstComment = new Comment() { ProductId = "1", Content = "First comment." };
            var secondComment = new Comment() { ProductId = "2", Content = "Second comment." };

            this.context.AddRange(firstComment, secondComment);
            await this.context.SaveChangesAsync();

            var userService = new Mock<IUserService>();
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var commentService = new CommentService(this.context, userService.Object, mapper);

            //Act

            var comments = await commentService.GetCommentsByProductId<CommentBindingModel>("1");

            //Assert

            Assert.Single(comments);
            Assert.Equal("First comment.", comments[0].Content);
        }

        [Fact]
        public async Task GetCommentsByProductIdUserNameMapping()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "chris" };
            var firstComment = new Comment() { ProductId = "1", Content = "First comment.", UserId = "user-id" };

            this.context.AddRange(user, firstComment);
            await this.context.SaveChangesAsync();

            var userService = new Mock<IUserService>();
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var commentService = new CommentService(this.context, userService.Object, mapper);

            //Act

            var comments = await commentService.GetCommentsByProductId<CommentBindingModel>("1");

            //Assert

            Assert.Equal("chris", comments.First().Username);
        }

        [Fact]
        public async Task GetCommentsByProductIdLastModifiedMapping()
        {
            //Arrange
            var lastModified = DateTime.ParseExact("01/02/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var firstComment = new Comment() { ProductId = "1", Content = "First comment.", LastModified = lastModified };

            await this.context.AddAsync(firstComment);
            await this.context.SaveChangesAsync();

            var userService = new Mock<IUserService>();
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var commentService = new CommentService(this.context, userService.Object, mapper);

            //Act

            var comments = await commentService.GetCommentsByProductId<CommentBindingModel>("1");

            //Assert

            Assert.Equal("01/02/2019", comments.First().LastModified);
        }

        [Fact]
        public async Task GetCommentsByProductIdWithInvalidId()
        {
            //Arrange

            var firstComment = new Comment() { ProductId = "1", Content = "First comment." };

            await this.context.AddAsync(firstComment);
            await this.context.SaveChangesAsync();

            var userService = new Mock<IUserService>();
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var commentService = new CommentService(this.context, userService.Object, mapper);

            //Act

            var comments = await commentService.GetCommentsByProductId<CommentBindingModel>("2");

            //Assert

            Assert.Empty(comments);
        }

        [Fact]
        public async Task AddComment()
        {
            //Arrange
            var user = new User() { Id = "user-id", UserName = "chris" };

            await this.context.AddAsync(user);
            await this.context.SaveChangesAsync();

            var mapper = new Mock<IMapper>();
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper.Object);
            var commentService = new CommentService(this.context, userService, mapper.Object);

            //Act

            await commentService.Add("chris", "product-id", "Comment.");
            var comment = this.context.Comments.First();

            //Assert

            Assert.Single(this.context.Comments);
            Assert.Equal("Comment.", comment.Content);
            Assert.Equal("product-id", comment.ProductId);
            Assert.Equal("user-id", comment.UserId);
        }

        [Fact]
        public async Task DeleteComment()
        {
            //Arrange

            await this.context.AddAsync(new Comment { Id = "product-id" });
            await this.context.SaveChangesAsync();

            var mapper = new Mock<IMapper>();
            var userService = new Mock<IUserService>();

            var commentService = new CommentService(this.context, userService.Object, mapper.Object);

            //Act

            await commentService.Delete("product-id");

            //Assert

            Assert.Empty(this.context.Comments);
        }
    }
}
