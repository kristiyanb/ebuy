namespace EBuy.Web.Areas.Admin.Models.Products
{
    using EBuy.Models;
    using EBuy.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class ProductInputModel : IMapTo<Product>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter a valid product name.")]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public IFormFile Image { get; set; }

        public string Description { get; set; }

        public int InStock { get; set; }

        public string CategoryName { get; set; }
    }
}
