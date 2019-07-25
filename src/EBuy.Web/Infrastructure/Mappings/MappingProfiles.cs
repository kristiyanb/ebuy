namespace EBuy.Web.Infrastructure.Mappings
{
    using AutoMapper;
    using EBuy.Models;
    using EBuy.Services.Models;
    using Areas.Admin.Models.Categories;
    using Areas.Admin.Models.Products;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProductInputModel, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<CategoryInputModel, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
