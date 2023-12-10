using Core.Consts;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string? Image { get; set; }

        public AccountType AccountType { get; set; }
        public virtual Doctor Doctor { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
    }
}