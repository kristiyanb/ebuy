namespace EBuy.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
    using Models.Users;
    using Microsoft.AspNetCore.Authorization;

    public class UsersController : AdminController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.userService.GetAll<UserDataModel>();

            return this.View(new UserListModel { Users = users });
        }

        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> ManageRoles()
        {
            return this.View();
        }

        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> RolesList()
        {
            var roles = await this.userService.GetUserRoleList();

            return this.View(roles);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> AddRole(string username, string roleName)
        {
            await this.userService.AddUserToRole(username, roleName);

            return this.Redirect("/Admin/Users/RolesList");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> RemoveRole(string username, string roleName)
        {
            await this.userService.RemoveUserFromRole(username, roleName);

            return this.Redirect("/Admin/Users/RolesList");
        }
    }
}
