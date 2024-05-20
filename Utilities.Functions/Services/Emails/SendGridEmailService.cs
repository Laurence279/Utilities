using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Utilities.Functions.Services.Emails
{
    public class SendGridEmailService : IEmailService
    {
        private readonly FromAddress fromAddress;
        private readonly SendGridClient client;

        public SendGridEmailService(IConfiguration configuration)
        {
            fromAddress = new FromAddress()
            {
                Address = configuration["EmailSettings__FromAddress"],
                Name = configuration["EmailSettings__FromName"]
            };
            client = new SendGridClient(configuration["EmailSettings__ApiKey"]);
        }
        public async Task SendAsync(Email email)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromAddress.Address, fromAddress.Name),
                Subject = email.Subject,
                PlainTextContent = email.PlainText,
                HtmlContent = email.Html
            };
            msg.AddTo(new EmailAddress(email.Addressee));
            try
            {
                await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
