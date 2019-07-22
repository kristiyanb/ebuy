﻿using EBuy.Models;
using EBuy.Services.Mapping;

namespace EBuy.Web.Areas.Admin.Models.Products
{
    public class ProductInputModel : IMapTo<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int InStock { get; set; }

        public string CategoryName { get; set; }
    }
}
