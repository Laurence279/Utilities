using Microsoft.Extensions.Configuration;
using Utilities.Functions.Services.Emails;
using System;

namespace Utilities.Functions.Services.Messages
{
    public class DeletedMessageHandler : IMessageHandler<DeletedMessage>
    {
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;
        public DeletedMessageHandler(IConfiguration configuration, IEmailService emailService)
        {
            this.configuration = configuration;
            this.emailService = emailService;
        }
        public async void Handle(DeletedMessage message)
        {
            var email = new Email()
            {
                Addressee = this.configuration["EmailSettings__AdminEmail"],
                Subject = "An item has been removed",
                PlainText = $"An item has been removed.\nId: {message.Id}\nName: {message.Name}\nDescription: {message.Desc}\nUserId: ${message.UserId}",
                Html = $"<strong>An item has been removed.</strong><br><p>Id: {message.Id}<br>Name: {message.Name}<br>Description: {message.Desc}<br>UserId: ${message.UserId}</p>"
            };
            await emailService.SendAsync(email);
            Console.WriteLine("Successfully sent email to " + email.Addressee);
        }
    }
}
