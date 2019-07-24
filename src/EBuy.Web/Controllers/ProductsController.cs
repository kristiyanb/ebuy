namespace EBuy.Web.Controllers
{
    using EBuy.Services.Contracts;
    using EBuy.Web.Models.Products;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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

            return View(product);
        }

        public async Task<IActionResult> Vote(string rating, string id)
        {
            await this.productService.UpdateRating(this.User.Identity.Name, id, rating);

            return Redirect("/Products/Details/" + id);
        }
    }
}
