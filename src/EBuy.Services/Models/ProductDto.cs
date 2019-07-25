namespace EBuy.Services.Models
{
    using Microsoft.AspNetCore.Http;

    public class ProductDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public IFormFile Image { get; set; }

        public string Description { get; set; }

        public int InStock { get; set; }

        public string CategoryName { get; set; }
    }
}
