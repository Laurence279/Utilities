using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utilities.Functions.Services.Messages
{
    public class MessageService : IMessageService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly Dictionary<Type, Type> handlers;

        public MessageService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            handlers = new Dictionary<Type, Type>();
            this.RegisterHandlers();
        }

        private void RegisterHandlers()
        {
            var handlerTypes = IMessageService.GetHandlers();

            foreach (var handlerType in handlerTypes)
            {
                var messageType = handlerType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMessageHandler<>))
                    .GetGenericArguments()[0];

                this.handlers[messageType] = handlerType;
            }
        }

        private dynamic Resolve(BaseMessage message)
        {
            if (handlers.TryGetValue(message.GetType(), out Type handlerType) && serviceProvider.GetService(handlerType) != null)
            {
                return serviceProvider.GetService(handlerType);
            }
            else
            {
                throw new InvalidOperationException($"No handler registered for message type: {message.GetType()}");
            }
        }

        public void Dispatch(BaseMessage message)
        {
            dynamic handler = Resolve(message);
            handler.Handle((dynamic)message);
        }
    }
}
