using System.Text.Json.Serialization;

namespace Core.Consts
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Day
    {
        Saturday = 0,
        Sunday = 1,
        Monday = 2,
        Tuesday = 3,
        Wednesday = 4,
        Thursday = 5,
        Friday = 6
    }
}