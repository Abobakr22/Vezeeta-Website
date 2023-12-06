using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.BookingDtos
{
    public class GetBookingDetailsDto
    {
        //[{image,doctorName,specialize,day,time,price,discoundCode,finalPrice,status }]
        public string Image { get; set; }
        public string DoctorName { get; set; }
        public string SpecializationName { get; set; }
        public double Price { get; set; }
        public DiscountCouponDto DiscountCoupon { get; set; } //DiscountCode
        public string BookingStatus { get; set; }
        public double FinalPrice { get; set; }
        public string Status { get; set; }
        public string Day { get; set; }
        public TimeSpan Time { get; set; }


    }
}
