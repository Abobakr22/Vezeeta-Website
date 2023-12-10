using System.Text.Json.Serialization;

namespace Core.Consts
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountType
    {
        Admin = 0,
        Patient = 1,
        Doctor = 2
    }
}
