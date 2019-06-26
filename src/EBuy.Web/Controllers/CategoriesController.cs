namespace EBuy.Web.Controllers
{
    using EBuy.Services;
    using EBuy.Web.Models.Categories;
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

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Products/{name}")]
        public async Task<IActionResult> Products(string name)
        {
            var productsFromDb = await this.categoryService
                .GetProductsByCategoryName(name);

            var products = productsFromDb.Select(x => new CategoryProductsViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price.ToString("F2"),
                Purchases = x.PurchasesCount,
                Rating = x.Score != 0 ? (x.Score / x.VotesCount).ToString("F1") : "0.0",
                InStock = x.InStock,
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
