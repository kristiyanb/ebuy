namespace EBuy.Web.Models.Categories
{
    using System.Collections.Generic;

    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            this.Products = new List<CategoryProductsViewModel>();
        }

        public string Name { get; set; }

        public List<CategoryProductsViewModel> Products { get; set; }
    }
}
