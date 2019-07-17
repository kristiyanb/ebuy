namespace EBuy.Web.ViewComponents
{
    using EBuy.Services.Contracts;
    using EBuy.Web.Models.Categories;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesDropdownViewComponent : ViewComponent
    {
        private readonly ICategoryService categoryService;

        public CategoriesDropdownViewComponent(ICategoryService categoryService)
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
