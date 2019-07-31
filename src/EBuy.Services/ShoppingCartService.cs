namespace EBuy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    using Contracts;
    using EBuy.Data;
    using EBuy.Models;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly EBuyDbContext context;
        private readonly IProductService productService;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public ShoppingCartService(EBuyDbContext context, 
            IProductService productService, 
            IUserService userService,
            IMapper mapper)
        {
            this.context = context;
            this.productService = productService;
            this.userService = userService;
            this.mapper = mapper;
        }

        public async Task<bool> AddProduct(string username, string id, int quantity)
        {
            var product = await this.productService.GetProductById(id);

            if (product == null)
            {
                return false;
            }

            var shoppingCart = await this.GetShoppingCart(username);
            var cartProduct = await this.context.ShoppingCartProducts.FirstOrDefaultAsync(x => x.Name == product.Name);

            if (cartProduct == null)
            {
                var newCartProduct = this.mapper.Map<ShoppingCartProduct>(product);

                newCartProduct.Quantity = quantity;
                newCartProduct.ShoppingCartId = shoppingCart.Id;

                await this.context.ShoppingCartProducts.AddAsync(newCartProduct);
            }
            else
            {
                cartProduct.Quantity += quantity;

                this.context.ShoppingCartProducts.Update(cartProduct);
            }

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveProduct(string id)
        {
            var product = await this.context.ShoppingCartProducts.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return false;
            }

            this.context.ShoppingCartProducts.Remove(product);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<List<ShoppingCartProduct>> GetShoppingCartProductsByUsername(string username) 
            => await this.context.ShoppingCartProducts
                .Where(x => x.ShoppingCart.User.UserName == username)
                .ToListAsync();

        public async Task<List<TViewModel>> GetShoppingCartProductsByUsername<TViewModel>(string username)
        {
            var shoppingCartProducts = await this.context.ShoppingCartProducts
                .Where(x => x.ShoppingCart.User.UserName == username)
                .ToListAsync();

            return this.mapper.Map<List<TViewModel>>(shoppingCartProducts);
        }

        public async Task<string> AddProductToGuestCart(string cart, string id, int quantity)
        {
            var productFromDb = await this.productService.GetProductById(id);

            if (productFromDb == null)
            {
                return cart;
            }

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

            var user = await this.userService.GetUserByUserName(username);

            await this.context.ShoppingCarts.AddAsync(new ShoppingCart { UserId = user.Id });
            await this.context.SaveChangesAsync();

            var newCart = await this.context.ShoppingCarts.FirstOrDefaultAsync(x => x.User.UserName == username);

            return newCart;
        }
    }
}