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

        public async Task<IActionResult> Result(string query)
        {
            var products = await this.productService.GetProductsByNameOrCategoryMatch<ProductGridModel>(query);

            return this.View(new SearchViewModel { Name = query, Products = products.ToList() });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
