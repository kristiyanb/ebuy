namespace EBuy.Web.Areas.Admin.Controllers
{
    using EBuy.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using EBuy.Models;
    using System.Linq;
    using Models.Products;
    using AutoMapper;
    using EBuy.Services.Models;

    public class ProductsController : AdminController
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return await this.Add();
            }

            var productDto = mapper.Map<ProductDto>(input);
            await this.productService.Add(productDto);

            return Redirect("/Admin/Dashboard/Index");
        }

        public async Task<IActionResult> Data()
        {
            var products = await this.productService.GetAll<ProductDetailsModel>();

            return View(new ProductsListModel { Products = products.ToList() });
        }

        public async Task<IActionResult> Deleted()
        {
            var products = await this.productService.GetDeleted<ProductDetailsModel>();

            return View(new ProductsListModel { Products = products.ToList() });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var product = await this.productService.GetProductById<ProductDetailsModel>(id);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductInputModel input)
        {
            var productDto = this.mapper.Map<ProductDto>(input);
            await this.productService.Edit(productDto);

            return Redirect("/Admin/Products/Edit?id=" + input.Id);
        }

        public async Task<IActionResult> Remove(string id)
        {
            await this.productService.Remove(id);

            return Redirect("/Admin/Products/Deleted");
        }

        public async Task<IActionResult> Restore(string id)
        {
            await this.productService.Restore(id);

            return Redirect("/Admin/Products/Data");
        }
    }
}
