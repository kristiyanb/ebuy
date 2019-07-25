namespace EBuy.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdminController
    {
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Index()
        {
            return this.View();
        }
    }
}
