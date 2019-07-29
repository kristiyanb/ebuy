namespace EBuy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using EBuy.Data;
    using EBuy.Models;
    using EBuy.Services.Models;

    public class MessageService : IMessageService
    {
        private readonly EBuyDbContext context;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly IEmailSender emailSender;

        public MessageService(EBuyDbContext context,
            IMapper mapper,
            IUserService userService,
            IEmailSender emailSender)
        {
            this.context = context;
            this.mapper = mapper;
            this.userService = userService;
            this.emailSender = emailSender;
        }

        public async Task Add(MessageDto input, string username)
        {
            var message = this.mapper.Map<Message>(input);

            message.isActive = true;
            message.SubmissionDate = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(username))
            {
                var user = await this.userService.GetUserByUserName(username);

                message.UserId = user?.Id;
            }

            await this.context.Messages.AddAsync(message);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<TViewModel>> GetArchivedMessages<TViewModel>()
        {
            var messages = await this.context.Messages
                .Where(x => x.isActive == false)
                .Include(x => x.Replier)
                .ToListAsync();

            return this.mapper.Map<List<TViewModel>>(messages);
        }

        public async Task<List<TViewModel>> GetPendingMessages<TViewModel>()
        {
            var messages = await this.context.Messages
                .Where(x => x.isActive == true)
                .ToListAsync();

            return this.mapper.Map<List<TViewModel>>(messages);
        }

        public async Task SendResponse(string adminUsername, string messageId, string response)
        {
            var message = await this.context.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
            var admin = await this.userService.GetUserByUserName(adminUsername);

            if (message != null && admin != null)
            {
                await this.emailSender.SendEmailAsync(message.Email, "Re: " + message.Subject, response);

                message.isActive = false;
                message.ReplyDate = DateTime.UtcNow;
                message.ReplierId = admin.Id;

                this.context.Messages.Update(message);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task<TViewModel> GetMessageById<TViewModel>(string id)
        {
            var message = await this.context.Messages.FirstOrDefaultAsync(x => x.Id == id);

            return this.mapper.Map<TViewModel>(message);
        }
    }
}
