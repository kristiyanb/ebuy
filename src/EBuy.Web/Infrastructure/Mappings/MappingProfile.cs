namespace EBuy.Web.Infrastructure.Mappings
{
    using System.Linq;

    using AutoMapper;

    using Areas.Admin.Models.Categories;
    using Areas.Admin.Models.Comments;
    using Areas.Admin.Models.Messages;
    using Areas.Admin.Models.Products;
    using Areas.Admin.Models.Purchases;
    using Areas.Admin.Models.Users;
    using Areas.Identity.Pages.Account.Manage;
    using EBuy.Models;
    using EBuy.Services.Models;
    using Models.Categories;
    using Models.Comments;
    using Models.Contacts;
    using Models.Products;
    using Models.ShoppingCart;
    using Models.Users;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductInputModel, ProductDto>();
            CreateMap<ProductEditModel, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<CategoryInputModel, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<MessageInputModel, MessageDto>();
            CreateMap<MessageDto, Message>();
            CreateMap<Category, CategoryGridModel>();
            CreateMap<Comment, CommentBindingModel>()
                .ForMember(x => x.LastModified, opt => opt.MapFrom(c => c.LastModified.ToString("dd/MM/yyyy")))
                .ForMember(x => x.Username, opt => opt.MapFrom(u => u.User.UserName));
            CreateMap<Product, ProductDetailsModel>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Score != 0 ? (x.Score / x.VotesCount) : 0.0));
            CreateMap<Product, ProductGridModel>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Score != 0 ? (x.Score / x.VotesCount) : 0.0));
            CreateMap<ShoppingCartProduct, ShoppingCartProductViewModel>();
            CreateMap<User, UserViewModel>();
            CreateMap<Category, CategoryDetailsModel>()
                .ForMember(x => x.Purchases, opt => opt.MapFrom(x => x.Products.Sum(p => p.PurchasesCount)))
                .ForMember(x => x.Score, opt => opt.MapFrom(x => x.Products.Sum(p => p.Score)))
                .ForMember(x => x.VotesCount, opt => opt.MapFrom(x => x.Products.Sum(p => p.VotesCount)));
            CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.LastModified, opt => opt.MapFrom(y => y.LastModified.ToString("dd/MM/yyyy")))
                .ForMember(x => x.UserName, opt => opt.MapFrom(y => y.User.UserName));
            CreateMap<Message, MessageViewModel>()
                .ForMember(x => x.SubmissionDate, opt => opt.MapFrom(y => y.SubmissionDate.ToString("dd/MM/yyyy")));
            CreateMap<Product, ProductDataModel>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Score != 0 ? (x.Score / x.VotesCount) : 0.0));
            CreateMap<Product, ProductEditModel>();
            CreateMap<Purchase, PurchaseViewModel>();
            CreateMap<PurchasedProduct, PurchaseProductViewModel>();
            CreateMap<User, UserDetailsModel>()
                .ForMember(x => x.RegisteredOn, opt => opt.MapFrom(x => x.RegisteredOn.ToString("dd/MM/yyyy")))
                .ForMember(x => x.LastOnline, opt => opt.MapFrom(x => x.LastOnline.ToString("dd/MM/yyyy")));
            CreateMap<Purchase, PurchaseViewModel>()
                    .ForMember(x => x.DateOfOrder, opt => opt.MapFrom(y => y.DateOfOrder.ToString("dd/MM/yyyy")));
            CreateMap<PurchasedProduct, PurchaseProductViewModel>();
            CreateMap<Purchase, PurchaseHistoryModel.PurchaseViewModel>();
            CreateMap<PurchasedProduct, PurchaseHistoryModel.PurchasedProductViewModel>();
        }
    }
}
