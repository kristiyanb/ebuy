namespace EBuy.Web.Controllers
{
    using EBuy.Services.Contracts;
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
            var categories = await this.categoryService.GetCategories<CategoryGridModel>();

            return View(new CategoriesIndexViewModel { Categories = categories.ToList() });
        }

        [Route("/Products/{name}")]
        public async Task<IActionResult> Products(string name, string orderBy)
        {
            var products = await this.categoryService
                .GetProductsByCategoryName<ProductGridModel>(name, orderBy);

            return View(new CategoryViewModel { Name = name, Products = products.ToList() });
        }
    }
}
