using Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class GetDoctorDto
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public double price { get; set; }
        public string Image { get; set; }
        public string SpecializationName { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

    }
}
