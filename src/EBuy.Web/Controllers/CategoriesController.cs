namespace EBuy.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
    using Models.Categories;
    using Models.Products;

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

            return this.View(new CategoriesIndexViewModel { Categories = categories.ToList() });
        }

        [Route("/Products/{name}")]
        public async Task<IActionResult> Products(string name, string orderBy)
        {
            var products = await this.categoryService
                .GetProductsByCategoryName<ProductGridModel>(name, orderBy);

            return this.View(new CategoryViewModel { Name = name, Products = products.ToList() });
        }
    }
}
