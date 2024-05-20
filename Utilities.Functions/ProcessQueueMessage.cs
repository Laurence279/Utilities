using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Utilities.Functions.Services.Messages;

namespace Utilities.Functions
{
    public class ProcessQueueMessage
    {
        private readonly IMessageService messageService;

        public ProcessQueueMessage(IMessageService messageContext)
        {
            this.messageService = messageContext;
        }

        [FunctionName("ProcessQueueMessage")]
        public async Task Run([QueueTrigger("messages", Connection = "AzureWebJobsStorage")] string messageJson, ILogger log)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Converters = { new MessageConverter() } };
            var message = JsonConvert.DeserializeObject<BaseMessage>(messageJson, settings);

            this.messageService.Dispatch(message);
            log.LogInformation("Processed message: " + message.Type);
        }
    }
}
