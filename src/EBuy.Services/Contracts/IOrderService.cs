namespace EBuy.Services.Contracts
{
    using System.Threading.Tasks;

    public interface IOrderService
    {
        Task<bool> Create();
    }
}
