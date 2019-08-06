namespace EBuy.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using EBuy.Common;
    using EBuy.Services.Contracts;
    using Models.Products;
    using Models.Categories;

    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [Route("/Products/{name}")]
        public async Task<IActionResult> Category(string name, string orderBy)
        {
            var products = await this.productService
                .GetProductsByCategoryName<ProductGridModel>(name, orderBy);

            return this.View(new CategoryViewModel { Name = name, Products = products });
        }

        public async Task<IActionResult> Details(string id)
        {
            var product = await this.productService.GetProductById<ProductDetailsModel>(id);

            return this.View(product);
        }

        [Authorize(Roles = GlobalConstants.UserOnlyAccess)]
        public async Task<IActionResult> Vote(string rating, string id)
        {
            if (this.User.Identity.Name == null)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            await this.productService.UpdateRating(this.User.Identity.Name, id, rating);

            return this.Redirect("/Products/Details/" + id);
        }
    }
}
