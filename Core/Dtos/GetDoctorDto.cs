using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    //[{image, fullName, email, phone, specialize, gender}]

    public class GetDoctorDto
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SpecializationName { get; set; }
        public Doctor Doctor { get; set; }

    }
}
