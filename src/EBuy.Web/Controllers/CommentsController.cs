namespace EBuy.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
    using Models.Comments;

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
    }
}
