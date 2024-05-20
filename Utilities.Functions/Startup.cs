using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utilities.Functions.Services.Emails;
using Utilities.Functions.Services.Messages;

[assembly: FunctionsStartup(typeof(Utilities.Functions.Startup))]

namespace Utilities.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddSingleton<IEmailService, SendGridEmailService>();
            builder.Services.AddSingleton<IConfiguration>(configuration);
            builder.Services.AddSingleton<IMessageService>(provider =>
            {
                var messageService = new MessageService(provider);
                return messageService;
            });

            var messageHandlerTypes = IMessageService.GetHandlers();
            foreach (var handlerType in messageHandlerTypes)
            {
                builder.Services.AddSingleton(handlerType);
            }
        }
    }
}
