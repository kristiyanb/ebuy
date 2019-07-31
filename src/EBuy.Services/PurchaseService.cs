namespace EBuy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using EBuy.Data;
    using EBuy.Models;

    public class PurchaseService : IPurchaseService
    {
        private readonly EBuyDbContext context;
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public PurchaseService(EBuyDbContext context,
            IUserService userService,
            IProductService productService,
            IMapper mapper)
        {
            this.context = context;
            this.userService = userService;
            this.productService = productService;
            this.mapper = mapper;
        }

        public async Task<Purchase> Add(string address, string username)
        {
            var user = await this.userService.GetUserByUserName(username);

            var purchase = new Purchase
            {
                Address = address,
                DateOfOrder = DateTime.UtcNow,
                UserId = user?.Id,
            };

            await this.context.Purchases.AddAsync(purchase);
            await this.context.SaveChangesAsync();

            return await this.context.Purchases
                .FirstOrDefaultAsync(x => x.DateOfOrder == purchase.DateOfOrder);
        }

        public async Task AddPurchasedProduct(ShoppingCartProduct product, Purchase purchase)
        {
            var purchasedProduct = this.mapper.Map<PurchasedProduct>(product);

            purchasedProduct.PurchaseId = purchase.Id;

            await this.productService.UpdateProductQuantityAndSales(
                purchasedProduct.Name,
                purchasedProduct.ImageUrl,
                purchasedProduct.Price,
                purchasedProduct.Quantity);

            await this.context.PurchasedProducts.AddAsync(purchasedProduct);
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
