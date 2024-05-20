using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Utilities.Functions.Services.Messages
{
    public interface IMessageHandler<T> where T : BaseMessage
    {
        public abstract void Handle(T message);
    }
}
