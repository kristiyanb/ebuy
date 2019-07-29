namespace EBuy.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
    using EBuy.Services.Models;
    using Models.Categories;

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
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return await this.Add();
            }

            var categoryDto = this.mapper.Map<CategoryDto>(input);

            await this.categoryService.Add(categoryDto);

            return this.Redirect("/Admin/Categories/Data");
        }

        public async Task<IActionResult> Data()
        {
            var categories = await this.categoryService.GetCategories<CategoryDetailsModel>();

            return this.View(new CategoryListModel { Categories = categories });
        }
    }
}
