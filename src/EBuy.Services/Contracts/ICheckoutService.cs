﻿namespace EBuy.Services.Contracts
{
    using System.Threading.Tasks;

    public interface ICheckoutService
    {
        Task<bool> Checkout(string username, string address);

        Task<bool> CheckoutAsGuest(string cart, string address);
    }
}
