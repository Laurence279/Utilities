using Microsoft.Extensions.Configuration;
using Utilities.Functions.Services.Emails;
using System;

namespace Utilities.Functions.Services.Messages
{
    public class CreatedMessageHandler : IMessageHandler<CreatedMessage>
    {
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;
        public CreatedMessageHandler(IEmailService emailService, IConfiguration configuration)
        {
            this.emailService = emailService;
            this.configuration = configuration;
        }

        public async void Handle(CreatedMessage message)
        {
            var email = new Email()
            {
                Addressee = configuration["EmailSettings__AdminEmail"],
                Subject = "An item has been created",
                PlainText = $"An item has been created.\nId: {message.Id}\nName: {message.Name}\nDescription: {message.Desc}\nUserId: ${message.UserId}",
                Html = $"<strong>An item has been created.</strong><br><p>Id: {message.Id}<br>Name: {message.Name}<br>Description: {message.Desc}<br>UserId: ${message.UserId}</p>"
            };
            await emailService.SendAsync(email);
            Console.WriteLine("Successfully sent email to " + email.Addressee);
        }
    }
}
