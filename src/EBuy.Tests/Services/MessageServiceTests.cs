namespace EBuy.Tests.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Moq;
    using Xunit;

    using EBuy.Data;
    using EBuy.Models;
    using EBuy.Services;
    using EBuy.Services.Contracts;
    using EBuy.Services.Models;
    using EBuy.Web.Areas.Admin.Models.Messages;
    using EBuy.Web.Infrastructure.Mappings;

    public class MessageServiceTests
    {
        private readonly EBuyDbContext context;

        public MessageServiceTests()
        {
            this.context = TestStartup.GetContext();
        }

        [Fact]
        public async Task AddMessageWithAuthenticatedUser()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "chris" };

            await this.context.AddAsync(user);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);
            var emailSender = new Mock<IEmailSender>();

            var messageService = new MessageService(this.context, mapper, userService, emailSender.Object);

            //Act

            var messageDto = new MessageDto()
            {
                Content = "Message",
                Email = "Email",
                Name = "Chris",
                Subject = "Subject"
            };

            await messageService.Add(messageDto, "chris");

            var message = this.context.Messages.First();

            //Assert

            Assert.True(message.isActive);
            Assert.Equal("Message", message.Content);
            Assert.Equal("Subject", message.Subject);
            Assert.Equal("Email", message.Email);
            Assert.Equal("chris", message.User.UserName);
        }

        [Fact]
        public async Task AddMessageWithUnauthenticatedUser()
        {
            //Arrange

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper);
            var emailSender = new Mock<IEmailSender>();

            var messageService = new MessageService(this.context, mapper, userService, emailSender.Object);

            //Act

            var messageDto = new MessageDto()
            {
                Content = "Message",
                Email = "Email",
                Name = "Chris",
                Subject = "Subject"
            };

            await messageService.Add(messageDto, "");

            var message = this.context.Messages.First();

            //Assert

            Assert.True(message.isActive);
            Assert.Equal("Message", message.Content);
            Assert.Equal("Subject", message.Subject);
            Assert.Equal("Email", message.Email);
            Assert.Null(message.User);
        }

        [Fact]
        public async Task GetPendingMessages()
        {
            //Arrange

            var firstMessage = new Message() { Id = "pending-message", isActive = true };
            var secondMessage = new Message() { Id = "archived-message", isActive = false };

            this.context.AddRange(firstMessage, secondMessage);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var userService = new Mock<IUserService>();
            var emailSender = new Mock<IEmailSender>();

            var messageService = new MessageService(this.context, mapper, userService.Object, emailSender.Object);

            //Act

            var pendingMessages = await messageService.GetPendingMessages<MessageViewModel>();

            //Assert

            Assert.Single(pendingMessages);
            Assert.Equal("pending-message", pendingMessages[0].Id);
        }

        [Fact]
        public async Task GetArchivedMessages()
        {
            //Arrange
            var user = new User() { Id = "user", UserName = "User" };
            var firstMessage = new Message() { Id = "pending-message", isActive = true };
            var secondMessage = new Message() { Id = "archived-message", isActive = false, Replier = user };

            this.context.AddRange(firstMessage, secondMessage);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var userService = new Mock<IUserService>();
            var emailSender = new Mock<IEmailSender>();

            var messageService = new MessageService(this.context, mapper, userService.Object, emailSender.Object);

            //Act

            var archivedMessages = await messageService.GetArchivedMessages<MessageViewModel>();

            //Assert

            Assert.Single(archivedMessages);
            Assert.Equal("archived-message", archivedMessages[0].Id);
            Assert.Equal("User", archivedMessages[0].ReplierUserName);
        }

        [Fact]
        public async Task SendResponse()
        {
            //Arrange
            var admin = new User() { Id = "admin", UserName = "Admin" };
            var firstMessage = new Message() { Id = "pending-message", isActive = true };

            this.context.AddRange(firstMessage, admin);
            await this.context.SaveChangesAsync();

            var mapper = new Mock<IMapper>();
            var emailSender = new Mock<IEmailSender>();
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var userService = new UserService(this.context, userManager.Object, mapper.Object);

            var messageService = new MessageService(this.context, mapper.Object, userService, emailSender.Object);

            //Act

            await messageService.SendResponse("Admin", "pending-message", "Response.");

            var message = this.context.Messages.First();

            //Assert

            Assert.False(message.isActive);
            Assert.Equal("admin", message.ReplierId);
        }

        [Fact] 
        public async Task GetMessageById()
        {
            //Arrange

            var dbMessage = new Message() { Id = "message-id", Content = "Content" };

            await this.context.AddAsync(dbMessage);
            await this.context.SaveChangesAsync();

            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            var userService = new Mock<IUserService>().Object;
            var emailSender = new Mock<IEmailSender>().Object;

            var messageService = new MessageService(this.context, mapper, userService, emailSender);

            //Act

            var message = await messageService.GetMessageById<MessageViewModel>("message-id");

            //Assert

            Assert.NotNull(message);
            Assert.Equal("Content", message.Content);
        }
    }
}
