using Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.AppointmentDtos
{
    public class UpdateAppointmentDto
    {
        public int AppointmentId { get; set; }
        //public string Day { get; set; }
        //public TimeSpan Time { get; set; }
        public List<Day> Days { get; set; }
        public List<long> times { get; set; }

    }
}
