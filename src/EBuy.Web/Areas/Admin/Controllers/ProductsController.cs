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
        private readonly ICloudinaryService cloudinaryService;

        public ProductsController(IProductService productService, ICategoryService categoryService, ICloudinaryService cloudinaryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return await this.Add();
            }

            var imageUrl = await this.cloudinaryService.UploadImage(input.Image, input.Name + "-image");

            var category = this.categoryService.GetCategoryByName(input.CategoryName);

            await this.productService.Add(new Product()
            {
                Name = input.Name,
                Description = input.Description,
                ImageUrl = imageUrl,
                Price = input.Price,
                InStock = input.InStock,
                CategoryId = category.Id
            });

            return Redirect("/Admin/Dashboard/Index");
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
            //TODO: Move everything to a service
            var category = this.categoryService.GetCategoryByName(input.CategoryName);
            var imageUrl = await this.cloudinaryService.UploadImage(input.Image, input.Name + "-image");

            var product = new Product()
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                ImageUrl = imageUrl,
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
