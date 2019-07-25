namespace EBuy.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using EBuy.Data;
    using Mapping;

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
