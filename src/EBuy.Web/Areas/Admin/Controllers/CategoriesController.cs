namespace EBuy.Web.Areas.Admin.Controllers
{
    using EBuy.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Models.Categories;
    using System.Linq;
    using AutoMapper;
    using EBuy.Services.Models;

    public class CategoriesController : AdminController
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryInputModel input)
        {
            var categoryDto = this.mapper.Map<CategoryDto>(input);
            await this.categoryService.Add(categoryDto);

            return Redirect("/Admin/Dashboard/Index");
        }

        public async Task<IActionResult> Data()
        {
            var categories = await this.categoryService.GetCategories<CategoryDetailsModel>();

            return View(new CategoryListModel { Categories = categories.ToList() });
        }
    }
}
