namespace EBuy.Web.Models.Categories
{
    using Models.Products;
    using System.Collections.Generic;

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
