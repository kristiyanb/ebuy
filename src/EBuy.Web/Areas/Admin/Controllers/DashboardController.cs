namespace EBuy.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using EBuy.Services.Contracts;
    using Models.Messages;

    public class DashboardController : AdminController
    {
        private readonly IMessageService messageService;

        public DashboardController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await this.messageService.GetPendingMessages<MessageViewModel>();

            return this.View(new MessageListModel { Messages = messages });
        }
    }
}
