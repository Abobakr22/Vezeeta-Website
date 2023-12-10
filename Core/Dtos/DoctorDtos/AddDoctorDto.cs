using Core.Consts;
using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.DoctorDtos
{
    public class AddDoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public Specialization Specialization { get; set; }
        public int SpecializationId { get; set; }
    }
}
