namespace EBuy.Web.Areas.Admin.Models.Messages
{
    using EBuy.Models;
    using EBuy.Services.Mapping;

    public class MessageViewModel : IMapFrom<Message>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
