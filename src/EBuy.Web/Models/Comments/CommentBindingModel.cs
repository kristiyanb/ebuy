namespace EBuy.Web.Models.Comments
{
    using AutoMapper;

    using EBuy.Models;

    public class CommentBindingModel
    {
        public string Content { get; set; }

        public string Username { get; set; }

        public string LastModified { get; set; }
    }
}
