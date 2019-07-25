namespace EBuy.Web.Models.ShoppingCart
{
    using System.Collections.Generic;
    using System.Linq;

    public class ShoppingCartViewModel
    {
        public List<ShoppingCartProductViewModel> Products { get; set; }

        public decimal TotalCost => this.Products.Sum(x => x.Price * x.Quantity);
    }
}
