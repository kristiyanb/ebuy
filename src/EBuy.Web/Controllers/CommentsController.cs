namespace EBuy.Web.Controllers
{
    using EBuy.Models;
    using EBuy.Services.Contracts;
    using EBuy.Web.Models.Comments;
    using EBuy.Web.Models.Products;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<CommentBindingModel>> GetCommentsForProduct(string id)
        {
            return await this.commentService.GetCommentsByProductId<CommentBindingModel>(id);
        }

        [HttpPost(Name = "Create")]
        [Route("create")]
        public async Task<ActionResult> Create(CommentInputModel input)
        {
            await this.commentService.Add(input.Username, input.ProductId, input.Content);

            return this.Ok();
        }

        //public async Task<IActionResult> Delete(string id)
        //{
        //    var comment = this.commentService.Delete(id);

        //    var product = this.productService.GetProductById(comment.ProductId);

        //    var productViewModel = new ProductDetailsModel()
        //    {
        //        Id = product.Id,
        //        Name = product.Name,
        //        Description = product.Description,
        //        ImageUrl = product.ImageUrl,
        //        InStock = product.InStock,
        //        Price = product.Price.ToString("F2"),
        //        Purchases = product.PurchasesCount,
        //        Rating = product.Score != 0 ? (product.Score / product.VotesCount) : 0.0,
        //        Comments = product.Comments.ToList()
        //    };

        //    return View("Views/Products/Details.cshtml", productViewModel);
        //}
    }
}
