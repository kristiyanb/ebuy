namespace EBuy.Web.Models.Categories
{
    using System.Collections.Generic;

    using Models.Products;

    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            this.Products = new List<ProductGridModel>();
        }

        public string Name { get; set; }

        public List<ProductGridModel> Products { get; set; }
    }
}
