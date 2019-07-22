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

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryInputModel input)
        {
            await this.categoryService.Add(new Category()
            {
                Name = input.Name
            });

            return View("Areas/Admin/Views/Dashboard/Index.cshtml");
        }

        public async Task<IActionResult> Data()
        {
            var categories = await this.categoryService.GetCategories<CategoryDetailsModel>();

            return View(new CategoryListModel { Categories = categories.ToList() });
        }
    }
}
