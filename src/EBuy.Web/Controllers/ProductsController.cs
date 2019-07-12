namespace EBuy.Web.Controllers
{
    using EBuy.Services;
    using EBuy.Web.Models.Products;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
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
    }
}
