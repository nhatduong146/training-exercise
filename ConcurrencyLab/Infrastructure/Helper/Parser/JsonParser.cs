using ConcurrencyLab.Extensions;

namespace ConcurrencyLab.Infrastructure.Helper.Parser
{
    public class JsonParser : IJsonParser
    {
        public T DeserializeJson<T>(string json)
        {
            return json.DeserializeJson<T>();
        }
    }
}
