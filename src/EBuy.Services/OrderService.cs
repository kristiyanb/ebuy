namespace EBuy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

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
            var invalidEntities = new List<string>();

            foreach (var kvp in input.Products)
            {
                var id = kvp.Key;
                var quantity = kvp.Value;

                var product = await this.productService.GetProductById(id);

                if (product == null || quantity < 1)
                {
                    invalidEntities.Add(id);
                }
            }

            invalidEntities.ForEach(x => input.Products.Remove(x));

            if (input.Products.Count == 0) return false;

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

            await this.context.Purchases.AddAsync(purchase);

            foreach (var kvp in input.Products)
            {
                var id = kvp.Key;
                var quantity = kvp.Value;

                var product = await this.productService.GetProductById(id);

                product.InStock -= quantity;
                product.PurchasesCount += quantity;

                var purchasedProduct = this.mapper.Map<PurchasedProduct>(product);

                purchasedProduct.Purchase = purchase;
                purchasedProduct.Quantity = quantity;

                this.context.Products.Update(product);
                await this.context.PurchasedProducts.AddAsync(purchasedProduct);
            }

            await this.context.SaveChangesAsync();

            return true;
        }
    }
}
