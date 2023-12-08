using Core.Consts;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Core.Models
{
    public class Booking
    {
        public int Id { get; set; }

        //nav property for 1-M relationship with Doctor
        public virtual Doctor Doctor { get; set; }
        public int DoctorId { get; set; }

        //nav property for 1-M relationship with patient
        public virtual ApplicationUser Patient { get; set; }
        public string ApplicationUserId { get; set; } 

        //nav property for 1-1 relationship with Appointment
        public virtual Appointment Appointment { get; set; }
        public int AppointmentId { get; set; }

        //nav property for 1-1 relationship with AppointmentHour
        public virtual AppointmentHour AppointmentHour { get; set; }
        [ForeignKey("Booking")]
        public int AppointmentHourId { get; set; }

        //type of Appointment
        public RequestType BookingType { get; set; }

        //[AllowNull]
        //[ForeignKey("DiscountCoupon")]
        public int? DiscountCouponId { get; set; }
        public virtual DiscountCoupon DiscountCoupon { get; set; }
        

        
    }

}
