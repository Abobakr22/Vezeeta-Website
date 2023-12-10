using System.Text.Json.Serialization;

namespace Core.Consts
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RequestType
    {
        Pending = 0,
        Cancelled = 1,
        Completed = 2
    }
}