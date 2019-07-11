namespace EBuy.Web.Models.Categories
{
    using EBuy.Models;
    using EBuy.Services.Mapping;

    public class CategoryGridModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public int ProductCount { get; set; }

        public string ImageUrl { get; set; }
    }
}