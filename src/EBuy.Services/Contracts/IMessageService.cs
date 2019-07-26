namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;

    public interface IMessageService
    {
        Task Add(MessageDto input, string username);

        Task<IEnumerable<TViewModel>> GetPendingMessages<TViewModel>();

        Task<IEnumerable<TViewModel>> GetArchivedMessages<TViewModel>();

        Task SendResponse(string messageId, string email, string subject, string response);

        Task<TViewModel> GetMessageById<TViewModel>(string id);
    }
}
