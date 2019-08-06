namespace EBuy.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using EBuy.Common;
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
        [Authorize(Roles = GlobalConstants.UserOnlyAccess)]
        public async Task<ActionResult> Create(CommentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.StatusCode(304);
            }

            await this.commentService.Add(input.Username, input.ProductId, input.Content);

            return this.StatusCode(201);
        }
    }
}
