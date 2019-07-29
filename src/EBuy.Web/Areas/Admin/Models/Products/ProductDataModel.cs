namespace EBuy.Web.Areas.Admin.Models.Products
{
    using System.Collections.Generic;

    using EBuy.Models;

    public class ProductDataModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public double Rating { get; set; }

        public int InStock { get; set; }

        public string ImageUrl { get; set; }

        public int PurchasesCount { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
