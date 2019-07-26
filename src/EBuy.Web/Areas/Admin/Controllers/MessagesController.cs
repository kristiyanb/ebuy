namespace EBuy.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
    using Models.Messages;

    public class MessagesController : AdminController
    {
        private readonly IMessageService messageService;

        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public async Task<IActionResult> Index(string status)
        {
            IEnumerable<MessageViewModel> messages;

            if (status == "Pending")
            {
                messages = await this.messageService.GetPendingMessages<MessageViewModel>();
            }
            else if (status == "Archived")
            {
                messages = await this.messageService.GetArchivedMessages<MessageViewModel>();
            }
            else
            {
                messages = new List<MessageViewModel>();
            }

            return this.View(new MessageListModel { Messages = messages.ToList() });
        }

        public async Task<IActionResult> Details(string id)
        {
            var message = await this.messageService.GetMessageById<MessageViewModel>(id);

            return this.View(message);
        }
    }
}
