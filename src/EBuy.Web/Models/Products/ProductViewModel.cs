﻿using EBuy.Models;
using System.Collections.Generic;

namespace EBuy.Web.Models.Products
{
    public class ProductViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string Rating { get; set; }

        public int InStock { get; set; }

        public string ImageUrl { get; set; }

        public int Purchases { get; set; }

        public List<Comment> Comments { get; set; }
    }
}