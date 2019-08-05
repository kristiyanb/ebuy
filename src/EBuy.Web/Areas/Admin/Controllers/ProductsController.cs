namespace EBuy.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
    using EBuy.Services.Models;
    using Models.Products;

    public class ProductsController : AdminController
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly ICommentService commentService;

        public ProductsController(IProductService productService, 
            IMapper mapper, 
            ICommentService commentService)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.commentService = commentService;
        }

        public async Task<IActionResult> Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return await this.Add();
            }

            var productDto = this.mapper.Map<ProductDto>(input);

            await this.productService.Add(productDto);

            return this.Redirect("/Admin/Products/Data");
        }

        public async Task<IActionResult> Data(string category)
        {
            var products = await this.productService.GetAll<ProductDataModel>(category);

            return this.View(new ProductsListModel { Products = products });
        }

        public async Task<IActionResult> Deleted()
        {
            var products = await this.productService.GetDeleted<ProductDataModel>();

            return this.View(new ProductsListModel { Products = products });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var product = await this.productService.GetProductById<ProductEditModel>(id);

            return this.View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return await this.Edit(input.Id);
            }

            var productDto = this.mapper.Map<ProductDto>(input);

            await this.productService.Edit(productDto);

            return this.Redirect("/Admin/Products/Edit?id=" + input.Id);
        }

        public async Task<IActionResult> Remove(string id)
        {
            await this.productService.Remove(id);

            return this.Redirect("/Admin/Products/Deleted");
        }

        public async Task<IActionResult> Restore(string id)
        {
            await this.productService.Restore(id);

            return this.Redirect("/Admin/Products/Data");
        }

        public async Task<IActionResult> RemoveComment(string commentId)
        {
            await this.commentService.Delete(commentId);

            return this.Redirect("/Admin/Products/Data");
        }
    }
}
