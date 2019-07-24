namespace EBuy.Web.Models.ShoppingCart
{
    using System.Linq;
    using System.Collections.Generic;

    public class ShoppingCartDropdownViewModel
    {
        public decimal TotalCost => this.Products.Sum(x => x.Price * x.Quantity);

        public List<ShoppingCartProductViewModel> Products { get; set; }
    }
}
