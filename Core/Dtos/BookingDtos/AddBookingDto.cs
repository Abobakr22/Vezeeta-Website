

namespace Core.Dtos.BookingDtos
{
    public class AddBookingDto
    {
        public int DoctorId { get; set; }
        public string PatientId { get; set; }
        public int appointmentId { get; set; }
        public int timeId { get; set; }

    }
}
