namespace EBuy.Services.Contracts
{
    using System.Threading.Tasks;

    using Models;

    public interface IOrderService
    {
        Task<bool> Create(OrderDto input, string username);
    }
}
