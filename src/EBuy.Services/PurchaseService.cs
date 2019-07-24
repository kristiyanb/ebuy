namespace EBuy.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using Contracts;
    using EBuy.Data;
    using EBuy.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class PurchaseService : IPurchaseService
    {
        private readonly EBuyDbContext context;

        public PurchaseService(EBuyDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TViewModel>> GetAll<TViewModel>()
            => await this.context.Purchases
                .OrderByDescending(x => x.DateOfOrder)
                .To<TViewModel>()
                .ToListAsync();
    }
}
