namespace EBuy.Web.Areas.Admin.Controllers
{
    using EBuy.Models;
    using EBuy.Services;
    using EBuy.Web.Areas.Admin.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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
        public async Task<IActionResult> AddCategory(CategoryInputModel input)
        {
            this.categoryService.Add(new Category()
            {
                Name = input.Name
            });

            return View("Areas/Admin/Views/Dashboard/Index.cshtml");
        }
    }
}
