using Core.Consts;

namespace Core.Dtos.AppointmentDtos
{
    public class AddApointmentDto
    {
        public double Price { get; set; }
        public List<Day> Days { get; set; }
        public List<long> times { get; set; }
        public int DoctorId { get; set; }

    }
}
