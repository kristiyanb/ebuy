namespace EBuy.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ShoppingCartController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
