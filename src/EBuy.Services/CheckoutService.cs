namespace EBuy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using Contracts;
    using EBuy.Data;
    using EBuy.Models;

    public class CheckoutService : ICheckoutService
    {
        private readonly EBuyDbContext context;

        public CheckoutService(EBuyDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> Checkout(string username, string address)
        {
            var shoppingCartProducts = this.context.ShoppingCartProducts
                .Where(x => x.ShoppingCart.User.UserName == username)
                .ToList();

            if (shoppingCartProducts.Count == 0)
            {
                return false;
            }

            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            var purchase = new Purchase
            {
                Address = address,
                DateOfOrder = DateTime.UtcNow,
                UserId = user.Id,
            };

            await this.context.AddAsync(purchase);

            foreach (var product in shoppingCartProducts)
            {
                var purchasedProduct = new PurchasedProduct
                {
                    Name = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = product.Quantity,
                    Purchase = purchase
                };

                var catelogProduct = await this.context.Products.FirstOrDefaultAsync(x => x.Name == purchasedProduct.Name && 
                                                                          x.Price == purchasedProduct.Price && 
                                                                          x.ImageUrl == purchasedProduct.ImageUrl);

                catelogProduct.InStock -= purchasedProduct.Quantity;
                catelogProduct.PurchasesCount += purchasedProduct.Quantity;

                await this.context.PurchasedProducts.AddAsync(purchasedProduct);
                this.context.Products.Update(catelogProduct);
                this.context.ShoppingCartProducts.Remove(product);
            }

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CheckoutAsGuest(string cart, string address)
        {
            var purchase = new Purchase
            {
                Address = address, 
                DateOfOrder = DateTime.UtcNow,
            };

            await this.context.AddAsync(purchase);

            var products = JsonConvert.DeserializeObject<List<ShoppingCartProduct>>(cart);

            foreach (var product in products)
            {
                var purchasedProduct = new PurchasedProduct
                {
                    Name = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = product.Quantity,
                    Purchase = purchase
                };

                var catelogProduct = await this.context.Products.FirstOrDefaultAsync(x => x.Name == purchasedProduct.Name &&
                                                                          x.Price == purchasedProduct.Price &&
                                                                          x.ImageUrl == purchasedProduct.ImageUrl);

                catelogProduct.InStock -= purchasedProduct.Quantity;
                catelogProduct.PurchasesCount += purchasedProduct.Quantity;

                await this.context.PurchasedProducts.AddAsync(purchasedProduct);
                this.context.Products.Update(catelogProduct);
            }

            await this.context.SaveChangesAsync();

            return true;
        }
    }
}
