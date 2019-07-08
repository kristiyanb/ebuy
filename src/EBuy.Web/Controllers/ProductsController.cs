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
            var product = await this.productService.GetProductById(id);

            var productViewModel = new ProductDetailsModel()
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                InStock = product.InStock,
                Price = product.Price.ToString("F2"),
                Purchases = product.PurchasesCount,
                Rating = product.Score != 0 ? (product.Score / product.VotesCount) : 0.0,
                Comments = product.Comments.ToList()
            };

            return View(productViewModel);
        }
    }
}
