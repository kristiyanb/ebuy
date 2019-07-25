namespace EBuy.Services.Models
{
    using Microsoft.AspNetCore.Http;

    public class CategoryDto
    {
        public string Name { get; set; }

        public IFormFile Image { get; set; }
    }
}
