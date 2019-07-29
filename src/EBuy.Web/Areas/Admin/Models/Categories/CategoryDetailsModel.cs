namespace EBuy.Web.Areas.Admin.Models.Categories
{
    public class CategoryDetailsModel
    {
        public string Name { get; set; }

        public int Purchases { get; set; }

        public double Score { get; set; }

        public int VotesCount { get; set; }

        public double AverageRating
        {
            get
            {
                if (this.Score == 0 || this.VotesCount == 0)
                {
                    return 0;
                }

                return this.Score / this.VotesCount;
            }
        }
    }
}
