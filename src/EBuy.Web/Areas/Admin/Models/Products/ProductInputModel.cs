﻿namespace EBuy.Web.Areas.Admin.Models.Products
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class ProductInputModel
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = "Product name cannot be empty.")]
        [StringLength(30, ErrorMessage = "Product name length must be between {2} and {1} characters.", MinimumLength = 5)]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.01", "100000", ErrorMessage = "Please enter a valid price.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please select an image.")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Product description cannot be empty.")]
        [StringLength(800, ErrorMessage = "Product description length must be between {2} and {1} characters.", MinimumLength = 15)]
        public string Description { get; set; }

        [Range(1, 10000, ErrorMessage = "Please enter a valid quantity.")]
        public int InStock { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
