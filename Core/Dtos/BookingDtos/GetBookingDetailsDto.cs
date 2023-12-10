using Core.Consts;
using Core.Models;

namespace Core.Dtos.BookingDtos
{
    public class GetBookingDetailsDto
    {
        //[{image,doctorName,specialize,day,time,price,discoundCode,finalPrice,status }]
        public string Image { get; set; }
        public string DoctorName { get; set; }
        public string SpecializationName { get; set; }
        public double Price { get; set; }
        public string DiscountCode { get; set; } 
        public string BookingStatus { get; set; }
        public double FinalPrice { get; set; }
        public string Day { get; set; }
        public TimeSpan Time { get; set; }
        //public Gender Gender { get; set; }
        public int Age { get; set; }
        public ApplicationUser Patient { get; set; }


    }
}
