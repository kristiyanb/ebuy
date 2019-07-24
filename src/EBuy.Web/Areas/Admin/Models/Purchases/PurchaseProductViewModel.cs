namespace EBuy.Web.Areas.Admin.Models.Purchases
{
    using EBuy.Models;
    using EBuy.Services.Mapping;

    public class PurchaseProductViewModel : IMapFrom<PurchasedProduct>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
