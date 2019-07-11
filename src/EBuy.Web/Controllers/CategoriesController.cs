﻿namespace EBuy.Web.Controllers
{
    using EBuy.Services;
    using EBuy.Web.Models.Categories;
    using EBuy.Web.Models.Products;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = this.categoryService.GetCategories();

            var categoriesViewModel = new CategoriesIndexViewModel()
            {
                Categories = categories.Select(x => new CategoryGridModel()
                {
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    ProductCount = x.Products.Count
                }).ToList()
            };

            return View(categoriesViewModel);
        }

        [Route("/Products/{name}")]
        public async Task<IActionResult> Products(string name, string orderBy)
        {
            var productsFromDb = this.categoryService
                .GetProductsByCategoryName(name, orderBy);

            var products = productsFromDb.Select(x => new ProductGridModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price.ToString("F2"),
                Rating = x.Score != 0 ? (x.Score / x.VotesCount) : 0,
                ImageUrl = x.ImageUrl
            })
            .ToList();

            var categoryViewModel = new CategoryViewModel
            {
                Name = name,
                Products = products
            };

            return View(categoryViewModel);
        }
    }
}
