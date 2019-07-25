namespace EBuy.Web.Areas.Admin.Controllers
{
    using EBuy.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Models.Users;
    using System.Linq;

    public class UsersController : AdminController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.userService.GetAll<UserDetailsModel>();

            return View(new UserListModel { Users = users.ToList() });
        }

        public async Task<IActionResult> ManageRoles()
        {
            return View();
        }

        public async Task<IActionResult> RolesList()
        {
            var roles = await this.userService.GetUserRoleList();

            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string username, string roleName)
        {
            await this.userService.AddUserToRole(username, roleName);

            return Redirect("/Admin/Users");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string username, string roleName)
        {
            await this.userService.RemoveUserFromRole(username, roleName);

            return Redirect("/Admin/Users");
        }
    }
}
