using Core.Consts;

namespace Core.Dtos
{
    public class AddApointmentDto
    {
        public Double Price { get; set; }
        public List<Day> Days { get; set; }
        public List<long> times { get; set; }
        public int DoctorId { get; set; }

    }
}
