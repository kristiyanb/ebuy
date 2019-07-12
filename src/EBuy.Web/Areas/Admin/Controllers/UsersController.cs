namespace EBuy.Web.Areas.Admin.Controllers
{
    using EBuy.Services;
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
    }
}
