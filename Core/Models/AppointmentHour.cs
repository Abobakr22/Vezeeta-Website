namespace Core.Models
{
    public class AppointmentHour
    {
        public int Id { get; set; }    
        public TimeSpan Time { get; set; }  // TimeSpan represents a duration of time

        //nav property for 1-M relationship with Appointment
        public virtual Appointment Appointment { get; set; }
        public int AppointmentId { get; set; }
    }
}