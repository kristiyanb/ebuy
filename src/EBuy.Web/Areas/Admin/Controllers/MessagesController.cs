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

        public async Task<IActionResult> Pending()
        {
            var messages = await this.messageService.GetPendingMessages<MessageViewModel>();

            return this.View(new MessageListModel { Messages = messages.ToList() });
        }

        public async Task<IActionResult> Archived()
        {
            var messages = await this.messageService.GetArchivedMessages<MessageViewModel>();

            return this.View(new MessageListModel { Messages = messages.ToList() });
        }

        public async Task<IActionResult> Details(string id)
        {
            var message = await this.messageService.GetMessageById<MessageViewModel>(id);

            return this.View(message);
        }

        public async Task<IActionResult> SendResponse(string messageId, string response)
        {
            await this.messageService.SendResponse(this.User.Identity.Name, messageId, response);

            return this.Redirect("/Admin/Messages/Pending");
        }
    }
}
