namespace EBuy.Web.Areas.Admin.Models.Categories
{
    using System.Linq;

    using AutoMapper;

    using EBuy.Models;
    using EBuy.Services.Mapping;

    public class CategoryDetailsModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public int Purchases { get; set; }

        public double Score { get; set; }

        public int VotesCount { get; set; }

        public double AverageRating
        {
            get
            {
                if (this.Score == 0 || this.VotesCount == 0)
                {
                    return 0;
                }

                return this.Score / this.VotesCount;
            }
        }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, CategoryDetailsModel>()
                .ForMember(x => x.Purchases, opt => opt.MapFrom(x => x.Products.Sum(p => p.PurchasesCount)))
                .ForMember(x => x.Score, opt => opt.MapFrom(x => x.Products.Sum(p => p.Score)))
                .ForMember(x => x.VotesCount, opt => opt.MapFrom(x => x.Products.Sum(p => p.VotesCount)));
        }
    }
}
