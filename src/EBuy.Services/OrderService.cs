namespace EBuy.Services
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using EBuy.Data;

    public class OrderService : IOrderService
    {
        private readonly EBuyDbContext context;

        public OrderService(EBuyDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Create()
        {
            return true;
        }
    }
}
