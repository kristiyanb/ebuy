namespace EBuy.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
    using Models.Products;

    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Details(string id)
        {
            var product = await this.productService.GetProductById<ProductDetailsModel>(id);

            return this.View(product);
        }

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
