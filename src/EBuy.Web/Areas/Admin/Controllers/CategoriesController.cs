namespace EBuy.Web.Areas.Admin.Controllers
{
    using EBuy.Models;
    using EBuy.Services.Contracts;
    using EBuy.Web.Areas.Admin.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Models.Categories;
    using System.Linq;

    public class CategoriesController : AdminController
    {
        private readonly ICategoryService categoryService;
        private readonly ICloudinaryService cloudinaryService;

        public CategoriesController(ICategoryService categoryService, ICloudinaryService cloudinaryService)
        {
            this.categoryService = categoryService;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryInputModel input)
        {
            var imageUrl = await this.cloudinaryService.UploadImage(input.Image, input.Name + "-image");

            await this.categoryService.Add(new Category()
            {
                Name = input.Name,
                ImageUrl = imageUrl
            });

            return Redirect("/Admin/Dashboard/Index");
        }

        public async Task<IActionResult> Data()
        {
            var categories = await this.categoryService.GetCategories<CategoryDetailsModel>();

            return View(new CategoryListModel { Categories = categories.ToList() });
        }
    }
}
