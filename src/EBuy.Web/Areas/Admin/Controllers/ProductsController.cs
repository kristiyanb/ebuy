namespace EBuy.Web.Areas.Admin.Controllers
{
    using EBuy.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using EBuy.Models;
    using System.Linq;
    using Models.Products;

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
        public async Task<IActionResult> Add(ProductInputModel input)
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

        public async Task<IActionResult> Deleted()
        {
            var products = await this.productService.GetDeleted<ProductDetailsModel>();

            return View(new ProductsListModel { Products = products.ToList() });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var product = await this.productService.GetProductById<ProductDetailsModel>(id);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductInputModel input)
        {
            var category = this.categoryService.GetCategoryByName(input.CategoryName);

            var product = new Product()
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                ImageUrl = input.ImageUrl,
                Price = input.Price,
                InStock = input.InStock,
                CategoryId = category.Id
            };

            await this.productService.Edit(product);

            return Redirect("/Admin/Products/Edit?id=" + product.Id);
        }

        public async Task<IActionResult> Remove(string id)
        {
            await this.productService.Remove(id);

            return Redirect("/Admin/Products/Deleted");
        }

        public async Task<IActionResult> Restore(string id)
        {
            await this.productService.Restore(id);

            return Redirect("/Admin/Products/Data");
        }
    }
}
