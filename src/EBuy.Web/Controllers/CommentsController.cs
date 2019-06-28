﻿namespace EBuy.Web.Controllers
{
    using EBuy.Services;
    using EBuy.Web.Models.Comments;
    using EBuy.Web.Models.Products;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IProductService productService;

        public CommentsController(ICommentService commentService, IProductService productService)
        {
            this.commentService = commentService;
            this.productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommentInputModel input)
        {
            await this.commentService.Add(input.Username, input.ProductId, input.Content);

            var product = await this.productService.GetProductById(input.ProductId);

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                InStock = product.InStock,
                Price = product.Price.ToString("F2"),
                Purchases = product.PurchasesCount,
                Rating = product.Score != 0 ? (product.Score / product.VotesCount).ToString("F1") : "0.0",
                Comments = product.Comments.ToList()
            };

            return View("Views/Products/Details.cshtml", productViewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var comment = await this.commentService.Delete(id);

            var product = await this.productService.GetProductById(comment.ProductId);

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                InStock = product.InStock,
                Price = product.Price.ToString("F2"),
                Purchases = product.PurchasesCount,
                Rating = product.Score != 0 ? (product.Score / product.VotesCount).ToString("F1") : "0.0",
                Comments = product.Comments.ToList()
            };

            return View("Views/Products/Details.cshtml", productViewModel);
        }
    }
}