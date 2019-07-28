namespace EBuy.Web.Areas.Admin.Models.Comments
{
    using AutoMapper;
    using EBuy.Models;
    using EBuy.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string UserName { get; set; }

        public string LastModified { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.LastModified, opt => opt.MapFrom(y => y.LastModified.ToString("dd/MM/yyyy")))
                .ForMember(x => x.UserName, opt => opt.MapFrom(y => y.User.UserName));
        }
    }
}
