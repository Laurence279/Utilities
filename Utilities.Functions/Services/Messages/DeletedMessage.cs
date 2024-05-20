

using System;

namespace Utilities.Functions.Services.Messages
{
    public class DeletedMessage : BaseMessage
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string UserId { get; set; }
    }
}
