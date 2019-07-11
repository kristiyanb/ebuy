namespace EBuy.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class DashboardController : AdminController
    {
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
