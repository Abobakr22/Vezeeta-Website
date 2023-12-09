using Core.Consts;

namespace Core.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public Day Day { get; set; }
        public virtual List<AppointmentHour> Hours { get; set; }

        //nav property for 1-M relationship with Doctor
        public virtual Doctor Doctor { get; set; }
        public int DoctorId { get; set; }

    }
}

