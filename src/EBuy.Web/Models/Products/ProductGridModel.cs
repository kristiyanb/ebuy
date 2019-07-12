namespace EBuy.Web.Models.Products
{
    using AutoMapper;
    using EBuy.Models;
    using EBuy.Services.Mapping;

    public class ProductGridModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public double Rating { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductGridModel>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Score != 0 ? (x.Score / x.VotesCount) : 0.0));
        }
    }
}
