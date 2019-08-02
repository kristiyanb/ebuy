namespace EBuy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using EBuy.Data;
    using EBuy.Models;
    using Models;

    public class OrderService : IOrderService
    {
        private readonly EBuyDbContext context;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IProductService productService;

        public OrderService(EBuyDbContext context, 
            IMapper mapper, 
            IUserService userService, 
            IProductService productService)
        {
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
            this.productService = productService;
        }

        public async Task<bool> Create(OrderDto input, string username)
        {
            var user = await this.userService.GetUserByUserName(username);
            var purchase = new Purchase()
            {
                Address = input.Address, 
                Email = input.Email,
                FullName = input.FullName,
                PhoneNumber = input.PhoneNumber,
                UserId = user?.Id,
                DateOfOrder = DateTime.UtcNow
            };

            //var purchase = this.mapper.Map<Purchase>(input);
            //purchase.UserId = user?.Id;
            //purchase.DateOfOrder = DateTime.UtcNow;

            await this.context.Purchases.AddAsync(purchase);

            foreach (var kvp in input.Products)
            {
                var id = kvp.Key;
                var quantity = kvp.Value;

                if (quantity == 0) continue;

                var product = await this.productService.GetProductById(id);
                var purchasedProduct = this.mapper.Map<PurchasedProduct>(product);

                purchasedProduct.Purchase = purchase;
                purchasedProduct.Quantity = quantity;

                product.InStock -= quantity;
                product.PurchasesCount += quantity;

                this.context.Products.Update(product);
                await this.context.PurchasedProducts.AddAsync(purchasedProduct);
            }

            await this.context.SaveChangesAsync();

            return true;
        }
    }
}
