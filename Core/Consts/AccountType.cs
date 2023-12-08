using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Consts
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountType
    {
        Admin = 0 ,
        Patient = 1 ,
        Doctor = 2 
    }
}
