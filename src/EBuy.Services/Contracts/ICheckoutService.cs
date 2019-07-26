namespace EBuy.Services.Contracts
{
    using System.Threading.Tasks;

    public interface ICheckoutService
    {
        Task Checkout(string username, string address);

        Task CheckoutAsGuest(string cart, string address);
    }
}
