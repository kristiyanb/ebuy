namespace EBuy.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CategoriesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
