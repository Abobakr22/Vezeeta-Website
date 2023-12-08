using Core.Consts;
using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.DoctorDtos
{
    public class GetAllDoctorsDto
    {
        //[{image,fullName,email,phone,specialize,price,gender,appointments:[{day,times:[{id,time}]}]}]

        //public int Id { get; set; }
        public string? Image { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SpecializationName { get; set; }
        public double Price { get; set; }
        public string Gender { get; set; }
        public Day Day { get; set; }
        public List<GetAppointmentDto> Appointments { get; set; }
        ////  public AppointmentHour AppointmentHour { get; set; }

        //public Doctor Doctor { get; set; }
    }
}
