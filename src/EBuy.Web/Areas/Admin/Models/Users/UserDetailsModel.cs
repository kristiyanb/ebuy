namespace EBuy.Web.Areas.Admin.Models.Users
{
    using AutoMapper;

    using EBuy.Models;
    using EBuy.Services.Mapping;

    public class UserDetailsModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string RegisteredOn { get; set; }

        public string LastOnline { get; set; }

        public int PurchaseHistoryCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, UserDetailsModel>()
                .ForMember(x => x.RegisteredOn, opt => opt.MapFrom(x => x.RegisteredOn.ToString("dd/MM/yyyy")))
                .ForMember(x => x.LastOnline, opt => opt.MapFrom(x => x.LastOnline.ToString("dd/MM/yyyy")));
        }
    }
}
