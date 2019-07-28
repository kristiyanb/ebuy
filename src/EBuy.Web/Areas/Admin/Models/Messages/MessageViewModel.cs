namespace EBuy.Web.Areas.Admin.Models.Messages
{
    using AutoMapper;
    using EBuy.Models;
    using EBuy.Services.Mapping;

    public class MessageViewModel : IMapFrom<Message>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public string SubmissionDate { get; set; }

        public string ReplierUserName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Message, MessageViewModel>()
                .ForMember(x => x.SubmissionDate, opt => opt.MapFrom(y => y.SubmissionDate.ToString("dd/MM/yyyy")));
        }
    }
}
