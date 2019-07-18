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
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;
    using System;

    public class HomeController : Controller
    {
        private readonly IProductService productService;
        private readonly IMemoryCache cache;

        public HomeController(IProductService productService, IMemoryCache cache)
        {
            this.productService = productService;
            this.cache = cache;
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

        public async Task<IActionResult> SearchResult()
        {
            if (!this.cache.TryGetValue("searchParam", out string searchParam))
            {
                return View(new SearchViewModel { Name = "", Products = new List<ProductGridModel>() });
            }

            var products = await this.productService.GetProductsByNameOrCategoryMatch<ProductGridModel>(searchParam);

            return View(new SearchViewModel { Name = searchParam, Products = products.ToList() });
        }

        public async Task<IActionResult> Search(string searchParam)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(5));

            this.cache.Set("searchParam", searchParam, cacheEntryOptions);

            return Redirect("/Home/SearchResult");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
