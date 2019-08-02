namespace EBuy.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using EBuy.Data;

    public class PurchaseService : IPurchaseService
    {
        private readonly EBuyDbContext context;
        private readonly IMapper mapper;

        public PurchaseService(EBuyDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<TViewModel>> GetAll<TViewModel>()
        {
            var purchases = await this.context.Purchases
                .Include(x => x.User)
                .Include(x => x.Products)
                .OrderByDescending(x => x.DateOfOrder)
                .ToListAsync();

            return this.mapper.Map<List<TViewModel>>(purchases);
        }

        public async Task<List<TViewModel>> GetUserPurchaseHistory<TViewModel>(string username)
        {
            var purchases = await this.context.Purchases
                .Include(x => x.Products)
                .Where(x => x.User.UserName == username)
                .OrderByDescending(x => x.DateOfOrder)
                .ToListAsync();

            return this.mapper.Map<List<TViewModel>>(purchases);
        }
    }
}
