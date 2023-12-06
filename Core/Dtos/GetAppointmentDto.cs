using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class GetAppointmentDto
    {
        public string Days { get; set; }
        public List<TimeSpan> Times { get; set; }
    }
}
