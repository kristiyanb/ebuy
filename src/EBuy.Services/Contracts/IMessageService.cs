namespace EBuy.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;

    public interface IMessageService
    {
        Task Add(MessageDto input, string username);

        Task<List<TViewModel>> GetPendingMessages<TViewModel>();

        Task<List<TViewModel>> GetArchivedMessages<TViewModel>();

        Task SendResponse(string adminUsername, string messageId, string response);

        Task<TViewModel> GetMessageById<TViewModel>(string id);
    }
}
