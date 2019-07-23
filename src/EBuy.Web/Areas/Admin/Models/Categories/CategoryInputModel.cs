namespace EBuy.Web.Areas.Admin.Models.Categories
{
    using Microsoft.AspNetCore.Http;

    public class CategoryInputModel
    {
        public string Name { get; set; }

        public IFormFile Image { get; set; }
    }
}
