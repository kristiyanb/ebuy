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
    using EBuy.Models;
    using EBuy.Web.Areas.Admin.Models.Users;
    using EBuy.Web.Infrastructure.Mappings;
    using EBuy.Web.Models.Users;

    public class UserServiceTests
    {
        private readonly EBuyDbContext context;

        public UserServiceTests()
        {
            this.context = TestStartup.GetContext();
        }

        [Fact]
        public async Task GetUserByUsername()
        {
            //Arrange

            var dbUser = new User() { Id = "user-id", UserName = "Username" };

            await this.context.AddAsync(dbUser);
            await this.context.SaveChangesAsync();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapper = new Mock<IMapper>().Object;

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var user = await userService.GetUserByUserName("Username");

            //Assert

            Assert.NotNull(user);
            Assert.Equal("user-id", user.Id);
        }

        [Fact]
        public async Task GetUserByUsernameGeneric()
        {
            //Arrange

            var dbUser = new User() { UserName = "Username", FirstName = "FirstName" };

            await this.context.AddAsync(dbUser);
            await this.context.SaveChangesAsync();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var user = await userService.GetUserByUserName<UserViewModel>("Username");

            //Assert

            Assert.NotNull(user);
            Assert.IsType<UserViewModel>(user);
            Assert.Equal("FirstName", user.FirstName);
        }

        [Fact]
        public async Task GetAll()
        {
            //Arrange

            var admin = new User() { Id = "admin-id", UserName = "admin" };
            var user = new User() { Id = "user-id", UserName = "Username" };
            var purchase = new Purchase() { UserId = "user-id" };

            await this.context.AddRangeAsync(admin, user, purchase);
            await this.context.SaveChangesAsync();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var users = await userService.GetAll<UserDataModel>();

            //Assert

            Assert.Single(users);
            Assert.IsType<UserDataModel>(users[0]);
            Assert.Equal("Username", users[0].UserName);
            Assert.Equal(1, users[0].PurchaseHistoryCount);
        }

        [Fact]
        public async Task SetFirstNameWithInvalidUsername()
        {
            //Arrange

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var result = await userService.SetFirstName("Username", "UpdatedName");

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task SetFirstNameWithValidUsername()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username", FirstName = "FirstName" };

            await this.context.AddAsync(user);
            await this.context.SaveChangesAsync();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var result = await userService.SetFirstName("Username", "UpdatedName");
            var updatedUser = this.context.Users.First();

            //Assert

            Assert.True(result);
            Assert.Equal("UpdatedName", updatedUser.FirstName);
        }

        [Fact]
        public async Task SetLastNameWithInvalidUsername()
        {
            //Arrange

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var result = await userService.SetLastName("Username", "UpdatedName");

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task SetLastNameWithValidUsername()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username", LastName = "LastName" };

            await this.context.AddAsync(user);
            await this.context.SaveChangesAsync();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var result = await userService.SetLastName("Username", "UpdatedName");
            var updatedUser = this.context.Users.First();

            //Assert

            Assert.True(result);
            Assert.Equal("UpdatedName", updatedUser.LastName);
        }

        [Fact]
        public async Task AddUserToRoleWithInvalidArguments()
        {
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var result = await userService.AddUserToRole("admin", "InvalidRoleName");

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task AddUserToRoleWithValidArguments()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username", LastName = "LastName" };
            var role = new Role() { Id = "role-id", Name = "Role" };

            await this.context.AddRangeAsync(user, role);
            await this.context.SaveChangesAsync();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var result = await userService.AddUserToRole("Username", "Role");

            //Assert

            Assert.True(result);
        }

        [Fact]
        public async Task RemoveUserFromRoleWithInvalidArguments()
        {
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var result = await userService.RemoveUserFromRole("admin", "InvalidRoleName");

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task RemoveUserFromRoleWithValidArguments()
        {
            //Arrange

            var user = new User() { Id = "user-id", UserName = "Username", LastName = "LastName" };
            var role = new Role() { Id = "role-id", Name = "Role" };

            await this.context.AddRangeAsync(user, role);
            await this.context.SaveChangesAsync();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var result = await userService.RemoveUserFromRole("Username", "Role");

            //Assert

            Assert.True(result);
        }

        [Fact]
        public async Task SetLastOnlineNowWithInvalidUsername()
        {
            //Arrange

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var result = await userService.SetLastOnlineNow("InvalidUsername");

            //Assert

            Assert.False(result);
        }

        [Fact]
        public async Task SetLastOnlineNowWithValidUsername()
        {
            //Arrange

            var user = new User() { UserName = "Username", LastOnline = DateTime.MinValue };

            await this.context.AddAsync(user);
            await this.context.SaveChangesAsync();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var result = await userService.SetLastOnlineNow("Username");

            //Assert

            Assert.True(result);
        }

        [Fact]
        public async Task GetUserRoleList()
        {
            //Arrange

            var firstUser = new User()
            {
                Id = "first-user-id",
                FirstName = "First",
                LastName = "First",
                Email = "FirstEmail"
            };

            var secondUser = new User()
            {
                Id = "second-user-id",
                FirstName = "Second", 
                LastName = "Second", 
                Email = "SecondEmail"
            };

            var role = new Role() { Id = "role-id", Name = "RoleName" };

            var firstUserRole = new IdentityUserRole<string>() { UserId = "first-user-id", RoleId = "role-id" };
            var secondUserRole = new IdentityUserRole<string>() { UserId = "second-user-id", RoleId = "role-id" };

            await this.context.AddRangeAsync(firstUser, secondUser, role, firstUserRole, secondUserRole);
            await this.context.SaveChangesAsync();

            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mapperConfig = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();

            var userService = new UserService(this.context, userManager, mapper);

            //Act

            var userRoles = await userService.GetUserRoleList();

            var firstRole = userRoles["RoleName"].Any(x => x.Contains("First First (FirstEmail)"));
            var secondRole = userRoles["RoleName"].Any(x => x.Contains("Second Second (SecondEmail)"));

            //Assert

            Assert.Equal(2, userRoles["RoleName"].Count);
            Assert.True(firstRole);
            Assert.True(secondRole);
        }
    }
}
