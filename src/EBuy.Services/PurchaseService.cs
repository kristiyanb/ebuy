namespace EBuy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using EBuy.Data;
    using Mapping;
    using EBuy.Models;

    public class PurchaseService : IPurchaseService
    {
        private readonly EBuyDbContext context;
        private readonly IUserService userService;
        private readonly IProductService productService;

        public PurchaseService(EBuyDbContext context, IUserService userService, IProductService productService)
        {
            this.context = context;
            this.userService = userService;
            this.productService = productService;
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

            return purchase;
        }

        public async Task AddPurchasedProduct(ShoppingCartProduct product, Purchase purchase)
        {
            var purchasedProduct = new PurchasedProduct
            {
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity,
                Purchase = purchase
            };

            await this.productService.UpdateProductQuantityAndSales(
                purchasedProduct.Name,
                purchasedProduct.ImageUrl,
                purchasedProduct.Price,
                purchasedProduct.Quantity);

            await this.context.PurchasedProducts.AddAsync(purchasedProduct);
        }

        public async Task<IEnumerable<TViewModel>> GetAll<TViewModel>()
            => await this.context.Purchases
                .OrderByDescending(x => x.DateOfOrder)
                .To<TViewModel>()
                .ToListAsync();
    }
}
