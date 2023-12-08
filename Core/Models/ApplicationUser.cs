using Core.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string? Image { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public AccountType AccountType { get; set; }

        //edit => deleted class : public virtual List<Booking> Bookings { get; set; }

        //nav prop for 1-M relationshup between a patient and DiscountCoupon
      

        //3/12
        public virtual Doctor Doctor { get; set; }

    }
}
