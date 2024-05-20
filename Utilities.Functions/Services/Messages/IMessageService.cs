using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utilities.Functions.Services.Messages
{
    public interface IMessageService
    {
        public void Dispatch(BaseMessage message);

        public static List<Type> GetHandlers()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMessageHandler<>)))
                .ToList();
        }
    }
}
