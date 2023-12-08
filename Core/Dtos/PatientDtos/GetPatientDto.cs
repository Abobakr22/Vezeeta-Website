using Core.Consts;
using Core.Dtos.BookingDtos;
using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.PatientDtos
{
    //[details:{image(if found),fullName,email,phone,gender,dateOfBirth},
    //requests:[{image, doctorName, specialize, day, time, price, discoundCode, finalPrice, status}]]

    public class GetPatientDto
    {
        public string? Image { get; set; }
        public string FullName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public List<GetBookingDetailsDto> requests { get; set; }

        //requests:[{image, doctorName, specialize, day, time, price, discoundCode, finalPrice, status}]]
        //public string? DoctorImage { get; set; }
        //public string DoctorName { get;set; }
        //public string Specialize { get; set; }
        //public double Price { get; set; }
        //public string? DiscountCode { get; set; }
        //public string BookingStatus { get; set; }
        //public double FinalPrice { get; set; }
        //public string Day { get; set; }
        //public TimeSpan Time { get; set; }
    }
}
