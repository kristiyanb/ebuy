using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EBuy.Web.Controllers
{
    public class ProductsController : Controller
    {
        public async Task<IActionResult> Details(string id)
        {
            return View();
        }
    }
}
