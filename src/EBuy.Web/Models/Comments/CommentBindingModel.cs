namespace EBuy.Web.Models.Comments
{
    using AutoMapper;
    using EBuy.Models;
    using EBuy.Services.Mapping;

    public class CommentBindingModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string Username { get; set; }

        public string LastModified { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentBindingModel>()
                .ForMember(x => x.LastModified, opt => opt.MapFrom(c => c.LastModified.ToString("dd/MM/yyyy")))
                .ForMember(x => x.Username, opt => opt.MapFrom(u => u.User.UserName));
        }
    }
}
