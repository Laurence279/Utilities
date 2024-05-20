using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Linq;

namespace Utilities.Functions.Services.Messages
{
    public class MessageConverter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(BaseMessage).IsAssignableFrom(typeToConvert);
        }

        public override object ReadJson(JsonReader reader, Type typeToConvert, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            string typeName = jsonObject["type"].ToObject<string>();
            Type targetType = Assembly.GetExecutingAssembly().GetTypes()
                       .FirstOrDefault(t => t.Name == typeName);
            var instance = Activator.CreateInstance(targetType);
            serializer.Populate(jsonObject.CreateReader(), instance);
            return instance;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
