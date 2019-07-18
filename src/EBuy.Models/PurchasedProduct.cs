namespace EBuy.Models
{
    public class PurchasedProduct
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public string PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
    }
}
