namespace EBuy.Web.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
    using Models.Categories;


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

            return this.View(new CategoriesDropdownViewModel { Categories = categories.ToList() });
        }
    }
}
