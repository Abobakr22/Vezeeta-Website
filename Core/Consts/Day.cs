using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Consts
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Day
    {
       Saturday = 0 ,
       Sunday = 1 ,
       Monday = 2 ,
       Tuesday = 3 ,
       Wednesday = 4 ,
       Thursday = 5 ,
       Friday = 6 

    }
}

