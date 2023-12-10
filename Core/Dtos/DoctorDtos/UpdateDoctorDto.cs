using Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.DoctorDtos
{
    public class UpdateDoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public string SpecializationName { get; set; }
        public string UserName { get; set; }


        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
    }
}