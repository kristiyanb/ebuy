namespace EBuy.Web.Models.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CommentInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(150)]
        public string Content { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
