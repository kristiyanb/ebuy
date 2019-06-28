namespace EBuy.Web.Controllers
{
    using EBuy.Models;
    using EBuy.Services;
    using EBuy.Web.Models.Admin;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private readonly ICategoryService categoryService;

        public AdminController(IAdminService adminService, ICategoryService categoryService)
        {
            this.adminService = adminService;
            this.categoryService = categoryService;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductInputModel input)
        {
            var category = await this.categoryService.GetCategoryByName(input.CategoryName);

            await this.adminService.AddProduct(new Product()
            {
                Name = input.Name,
                Description = input.Description,
                ImageUrl = input.ImageUrl,
                Price = input.Price,
                InStock = input.InStock,
                CategoryId = category.Id
            });

            return View("Views/Admin/Dashboard.cshtml");
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryInputModel input)
        {
            await adminService.AddCategory(new Category()
            {
                Name = input.Name
            });

            return View("Views/Admin/Dashboard.cshtml");
        }

        public IActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(CompanyInputModel input)
        {
            await adminService.AddCompany(new Company()
            {
                Name = input.Name,
                Address = input.Address,
                Description = input.Description,
                Email = input.Description,
                PhoneNumber = input.PhoneNumber
            });

            return View("Views/Admin/Dashboard.cshtml");
        }
    }
}
