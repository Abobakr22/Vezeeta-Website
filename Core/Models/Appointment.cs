using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public Day Day { get; set; }
        
        //public DateTime AppointmentDate { get; set; }
        public virtual List<AppointmentHour> Hours { get; set; }

        //nav property for 1-M relationship with Doctor
        public virtual Doctor Doctor { get; set; }
        public int DoctorId { get; set; }

        //[Required]
        //[MaxLength(20)]
        //public string Status { get; set; } // e.g., Available, scheduled, canceled, completed
        //type of Appointment
        //public RequestType BookingType { get; set; }

    }
}

