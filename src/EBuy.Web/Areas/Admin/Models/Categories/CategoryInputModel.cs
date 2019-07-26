namespace EBuy.Web.Areas.Admin.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CategoryInputModel
    {

        [Required(ErrorMessage = "Category name cannot be empty.")]
        [StringLength(15, ErrorMessage = "Category name length must be between {2} and {1} characters.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select an image.")]
        public IFormFile Image { get; set; }
    }
}
