using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AppointmentHour
    {
        public int Id { get; set; }
        // TimeSpan represents a duration of time.
        public TimeSpan Time { get; set; }
         
        //nav property for 1-M relationship with Appointment
        public virtual Appointment Appointment { get; set; }
        public int AppointmentId { get; set; }


    }
}

//var appointmentHour = new AppointmentHour
//{
//    StartTime = TimeSpan.FromHours(9),   // Representing 9:00 AM
//    EndTime = TimeSpan.FromHours(11.5)   // Representing 11:30 AM
//};
