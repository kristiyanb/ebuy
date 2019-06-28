namespace EBuy.Web.Models.Search
{
    using EBuy.Web.Models.Products;
    using System.Collections.Generic;

    public class SearchViewModel
    {
        public string Name { get; set; }

        public List<ProductGridModel> Products { get; set; }
    }
}
