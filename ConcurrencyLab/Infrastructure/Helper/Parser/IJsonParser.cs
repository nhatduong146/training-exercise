namespace ConcurrencyLab.Infrastructure.Helper.Parser
{
    public interface IJsonParser
    {
        T DeserializeJson<T>(string json);
    }
}
