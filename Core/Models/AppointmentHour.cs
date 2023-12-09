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

