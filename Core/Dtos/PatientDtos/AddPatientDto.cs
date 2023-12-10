using Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.PatientDtos
{
    public class AddPatientDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string? Image { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
    }
}