namespace EBuy.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using EBuy.Common;
    using EBuy.Services.Contracts;
    using Models;
    using Models.Products;
    using Models.Search;


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

            return this.View(new ProductsCarouselModel { Products = products.ToList() });
        }

        public async Task<IActionResult> Contacts()
        {
            return this.View();
        }

        public async Task<IActionResult> ConfirmEmail()
        {
            return this.View();
        }

        public async Task<IActionResult> SearchResult()
        {
            if (!this.cache.TryGetValue(GlobalConstants.SearchParamKey, out string searchParam))
            {
                return this.View(new SearchViewModel { Name = "", Products = new List<ProductGridModel>() });
            }

            var products = await this.productService.GetProductsByNameOrCategoryMatch<ProductGridModel>(searchParam);

            return this.View(new SearchViewModel { Name = searchParam, Products = products.ToList() });
        }

        public async Task<IActionResult> Search(string searchParam)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10));

            this.cache.Set(GlobalConstants.SearchParamKey, searchParam, cacheEntryOptions);

            return Redirect("/Home/SearchResult");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
