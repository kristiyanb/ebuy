namespace EBuy.Web.Areas.Admin.ViewComponents
{
    using EBuy.Services.Contracts;
    using EBuy.Web.Areas.Admin.Models.Categories;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryListViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public CategoryListViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await this.categoryService.GetCategoryNames();

            return View(new CategoriesDropdownViewModel { Categories = categories.ToList() });
        }
    }
}
