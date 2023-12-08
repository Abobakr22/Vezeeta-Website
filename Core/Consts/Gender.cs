using System.Text.Json.Serialization;

namespace Core.Consts
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
       Female = 0 ,
       Male = 1
    }

}
