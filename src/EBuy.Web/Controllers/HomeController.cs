namespace EBuy.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using EBuy.Web.Models;
    using EBuy.Web.Models.Search;
    using EBuy.Web.Models.Products;
    using EBuy.Services.Contracts;
    using System.Threading.Tasks;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await this.productService.GetLastFiveProducts<ProductGridModel>();

            return View(new ProductsCarouselModel { Products = products.ToList() });
        }

        public async Task<IActionResult> Contacts()
        {
            return View();
        }

        public async Task<IActionResult> SearchResult(string searchParam, string orderBy)
        {
            var products = await this.productService.GetProductsByNameOrCategoryMatch<ProductGridModel>(searchParam);

            return View(new SearchViewModel { Name = searchParam, Products = products.ToList() });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
