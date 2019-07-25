﻿namespace EBuy.Services
{
    using EBuy.Data;
    using EBuy.Models;
    using EBuy.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Newtonsoft.Json;
    using System;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly EBuyDbContext context;
        private readonly IUserService userService;

        public ShoppingCartService(EBuyDbContext context, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
        }

        public async Task AddProduct(string username, string id, int quantity)
        {
            var product = await this.context.Products.FirstOrDefaultAsync(x => x.Id == id);
            var shoppingCart = await this.GetShoppingCart(username);

            var cartProduct = this.context.ShoppingCartProducts.FirstOrDefault(x => x.Name == product.Name);

            if (cartProduct != null)
            {
                cartProduct.Quantity += quantity;

                this.context.ShoppingCartProducts.Update(cartProduct);
            }
            else
            {
                var newCartProduct = new ShoppingCartProduct
                {
                    Name = product.Name,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Quantity = quantity,
                    ShoppingCartId = shoppingCart.Id
                };

                await this.context.ShoppingCartProducts.AddAsync(newCartProduct);
            }

            await this.context.SaveChangesAsync();
        }

        public async Task RemoveProduct(string id)
        {
            var product = await this.context.ShoppingCartProducts.FirstOrDefaultAsync(x => x.Id == id);

            this.context.ShoppingCartProducts.Remove(product);
            await this.context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TViewModel>> GetShoppingCartProductsByUsername<TViewModel>(string username)
            => await this.context.ShoppingCartProducts
                .Where(x => x.ShoppingCart.User.UserName == username)
                .To<TViewModel>()
                .ToListAsync();

        public async Task<string> AddProductToGuestCart(string cart, string id, int quantity)
        {
            var productFromDb = await this.context.Products.FirstOrDefaultAsync(x => x.Id == id);
            List<GuestCartProduct> products;

            if (cart == null)
            {
                products = new List<GuestCartProduct>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<GuestCartProduct>>(cart);

                var product = products.FirstOrDefault(x => x.Name == productFromDb.Name);

                if (product != null)
                {
                    product.Quantity += quantity;

                    return JsonConvert.SerializeObject(products);
                }
            }

            var newProduct = new GuestCartProduct()
            {
                Id = Guid.NewGuid().ToString(),
                Name = productFromDb.Name,
                ImageUrl = productFromDb.ImageUrl,
                Price = productFromDb.Price,
                Quantity = quantity
            };

            products.Add(newProduct);

            return JsonConvert.SerializeObject(products);
        }

        private async Task<ShoppingCart> GetShoppingCart(string username)
        {
            var shoppingCart = await this.context.ShoppingCarts.FirstOrDefaultAsync(x => x.User.UserName == username);

            if (shoppingCart != null)
            {
                return shoppingCart;
            }

            var user = await this.context.Users.FirstOrDefaultAsync(x => x.UserName == username);

            await this.context.ShoppingCarts.AddAsync(new ShoppingCart { UserId = user.Id });
            await this.context.SaveChangesAsync();

            return await this.context.ShoppingCarts.FirstOrDefaultAsync(x => x.User.UserName == username);
        }
    }
}
