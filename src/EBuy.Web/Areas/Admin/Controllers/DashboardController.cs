namespace EBuy.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
