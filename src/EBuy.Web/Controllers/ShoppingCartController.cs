namespace EBuy.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
    using Models.ShoppingCart;

    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IProductService productService;

        public ShoppingCartController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ShoppingCartProductViewModel> GetProduct(string id)
        {
            return await this.productService.GetProductById<ShoppingCartProductViewModel>(id);
        }
    }
}
