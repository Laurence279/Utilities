using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.Extensions.Configuration;

namespace Utilities.Functions
{
    public class ProcessQueueMessage
    {
        private readonly IConfiguration configuration;

        public ProcessQueueMessage(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [FunctionName("ProcessQueueMessage")]
        public async Task Run([QueueTrigger("messages", Connection = "AzureWebJobsStorage")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var apiKey = this.configuration["EmailSettings__ApiKey"];
            var from = (Address: this.configuration["EmailSettings__FromAddress"], Name: this.configuration["EmailSettings__FromName"]);
            var to = this.configuration["EmailSettings__ToAddress"];

            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(from.Address, from.Name),
                Subject = "New message",
                PlainTextContent = "A new message has been added.",
                HtmlContent = "<strong>A new message has been added.</strong>"
            };
            msg.AddTo(new EmailAddress(to));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
