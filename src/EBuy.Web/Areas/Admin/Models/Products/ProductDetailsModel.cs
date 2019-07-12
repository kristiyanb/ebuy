namespace EBuy.Web.Areas.Admin.Models
{
    using AutoMapper;
    using EBuy.Models;
    using EBuy.Services.Mapping;
    using System.Collections.Generic;

    public class ProductDetailsModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public double Rating { get; set; }

        public int InStock { get; set; }

        public string ImageUrl { get; set; }

        public int PurchasesCount { get; set; }

        public List<Comment> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, ProductDetailsModel>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Score != 0 ? (x.Score / x.VotesCount) : 0.0));
        }
    }
}
