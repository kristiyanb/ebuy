namespace EBuy.Web.Models.Comments
{
    using System;

    public class CommentInputModel
    {
        public DateTime LastModified { get; set; }

        public string Content { get; set; }

        public string ProductId { get; set; }

        public string UserId { get; set; }
    }
}
