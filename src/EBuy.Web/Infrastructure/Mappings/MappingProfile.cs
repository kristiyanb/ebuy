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
    using EBuy.Common;
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
            //Products 

            CreateMap<ProductInputModel, ProductDto>();

            CreateMap<ProductEditModel, ProductDto>();

            CreateMap<ProductDto, Product>();

            CreateMap<Product, ProductEditModel>();
            CreateMap<Product, ShoppingCartProductViewModel>();
            CreateMap<Product, ShoppingCartProduct>();
            CreateMap<Product, ProductDetailsModel>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Score != 0 ? (x.Score / x.VotesCount) : 0.0));
            CreateMap<Product, ProductGridModel>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Score != 0 ? (x.Score / x.VotesCount) : 0.0));
            CreateMap<Product, ProductDataModel>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Score != 0 ? (x.Score / x.VotesCount) : 0.0));

            CreateMap<ShoppingCartProduct, PurchasedProduct>();
            CreateMap<ShoppingCartProduct, ShoppingCartProductViewModel>();


            //Categories

            CreateMap<CategoryInputModel, CategoryDto>();

            CreateMap<CategoryDto, Category>();

            CreateMap<Category, CategoryGridModel>();
            CreateMap<Category, CategoryDetailsModel>()
                .ForMember(x => x.Purchases, opt => opt.MapFrom(x => x.Products.Sum(p => p.PurchasesCount)))
                .ForMember(x => x.Score, opt => opt.MapFrom(x => x.Products.Sum(p => p.Score)))
                .ForMember(x => x.VotesCount, opt => opt.MapFrom(x => x.Products.Sum(p => p.VotesCount)));


            //Messages

            CreateMap<MessageInputModel, MessageDto>();

            CreateMap<MessageDto, Message>();

            CreateMap<Message, MessageViewModel>()
                .ForMember(x => x.SubmissionDate, opt => opt.MapFrom(x => x.SubmissionDate.ToString(GlobalConstants.DateFormat)));

            //Comments

            CreateMap<Comment, CommentBindingModel>()
                .ForMember(x => x.LastModified, opt => opt.MapFrom(x => x.LastModified.ToString(GlobalConstants.DateFormat)))
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.User.UserName));
            CreateMap<Comment, CommentViewModel>()
                .ForMember(x => x.LastModified, opt => opt.MapFrom(x => x.LastModified.ToString(GlobalConstants.DateFormat)))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName));


            //Users

            CreateMap<User, UserViewModel>();
            CreateMap<User, UserDataModel>()
                .ForMember(x => x.RegisteredOn, opt => opt.MapFrom(x => x.RegisteredOn.ToString(GlobalConstants.DateFormat)))
                .ForMember(x => x.LastOnline, opt => opt.MapFrom(x => x.LastOnline.ToString(GlobalConstants.DateFormat)));

            //Purchases

            CreateMap<Purchase, PurchaseHistoryModel.PurchaseViewModel>()
                .ForMember(x => x.DateOfOrder, opt => opt.MapFrom(x => x.DateOfOrder.ToString(GlobalConstants.DateFormat)));
            CreateMap<Purchase, PurchaseViewModel>()
                .ForMember(x => x.DateOfOrder, opt => opt.MapFrom(x => x.DateOfOrder.ToString(GlobalConstants.DateFormat)));

            CreateMap<PurchasedProduct, PurchaseProductViewModel>();
            CreateMap<PurchasedProduct, PurchaseHistoryModel.PurchasedProductViewModel>();
            CreateMap<PurchasedProduct, PurchaseProductViewModel>();
        }
    }
}
