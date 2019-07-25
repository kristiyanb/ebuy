namespace EBuy.Web.Models.Search
{
    using System.Collections.Generic;

    using Models.Products;

    public class SearchViewModel
    {
        public string Name { get; set; }

        public List<ProductGridModel> Products { get; set; }
    }
}
