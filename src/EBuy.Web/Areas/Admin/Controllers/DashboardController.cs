namespace EBuy.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdminController
    {
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
