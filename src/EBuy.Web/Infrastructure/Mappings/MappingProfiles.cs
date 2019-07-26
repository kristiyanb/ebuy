namespace EBuy.Web.Infrastructure.Mappings
{
    using AutoMapper;

    using Areas.Admin.Models.Categories;
    using Areas.Admin.Models.Products;
    using EBuy.Models;
    using EBuy.Services.Models;
    using Models.Contacts;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProductInputModel, ProductDto>();
            CreateMap<ProductEditModel, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<CategoryInputModel, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<MessageInputModel, MessageDto>();
            CreateMap<MessageDto, Message>();
        }
    }
}
