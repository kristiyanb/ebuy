namespace EBuy.Models
{
    public class Vote
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string ProductId { get; set; }

        public int Score { get; set; }
    }
}
