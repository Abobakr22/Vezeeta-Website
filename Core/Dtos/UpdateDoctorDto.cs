using Core.Consts;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class UpdateDoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public Specialization Specialization { get; set; }
        public int SpecializationId { get; set; }

    }
}
