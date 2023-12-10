using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public double Price { get; set; }

        //navigation property for Application User

        [ForeignKey("ApplicationUsers")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUsers { get; set; }

        //navigation property for specialization
        public virtual Specialization Specialization { get; set; }
        public virtual List<Booking> Requests { get; set; }
        public virtual List<Appointment> Appointments { get; set; }
        public int SpecializationId { get; set; }
    }
}