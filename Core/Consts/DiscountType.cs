using System.Text.Json.Serialization;

namespace Core.Consts
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DiscountType
    {
        Percentage = 0,
        Value = 1
    }
}