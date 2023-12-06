using Core.Consts;
using Core.Models;

namespace Core.Dtos.DoctorDtos
{
    public class AddDoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public Specialization Specialization { get; set; }
        public int SpecializationId { get; set; }



        //public List<Appointment> Appointments {  get; set; } 
        //public List<AppointmentHour> AppointmentsHour { get; set; }


        //{image,firstName,lastName,email,phone,specialize,gender,dateOfBirth }
        //Object Of(Price & List Of Days(enum) Each Day have List Of Time)

    }
}
