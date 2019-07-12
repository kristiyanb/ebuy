namespace EBuy.Web.Areas.Admin.Controllers
{
    using EBuy.Services;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using EBuy.Models;
    using System.Linq;

    public class ProductsController : AdminController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductInputModel input)
        {
            var category = this.categoryService.GetCategoryByName(input.CategoryName);

            await this.productService.Add(new Product()
            {
                Name = input.Name,
                Description = input.Description,
                ImageUrl = input.ImageUrl,
                Price = input.Price,
                InStock = input.InStock,
                CategoryId = category.Id
            });

            return View("Areas/Admin/Views/Dashboard/Index.cshtml");
        }

        public async Task<IActionResult> Data()
        {
            var products = await this.productService.GetAll<ProductDetailsModel>();

            return View(new ProductsListModel { Products = products.ToList() });
        }
    }
}
