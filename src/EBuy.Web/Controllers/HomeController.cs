﻿namespace EBuy.Web.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
    using EBuy.Web.Models;
    using EBuy.Web.Models.Search;
    using EBuy.Web.Models.Products;
    using EBuy.Services;
    using System.Threading.Tasks;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public async  Task<IActionResult> SearchResult(string searchParam, string orderBy)
        {
            var products = await this.productService.GetProductsByNameOrCategoryMatch(searchParam);

            var searchViewModel = new SearchViewModel()
            {
                Name = searchParam,
                Products = products.Select(x => new ProductGridModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price.ToString("F2"),
                    Rating = x.Score != 0 ? (x.Score / x.VotesCount) : 0,
                    ImageUrl = x.ImageUrl
                }).ToList()
            };

            return View(searchViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
