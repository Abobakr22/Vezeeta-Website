using Core.Consts;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.BookingDtos
{
    public class AddBookingDto
    {
        public int DoctorId { get; set; }
        public string PatientId { get; set; }
        public int appointmentId { get; set; }
        public int timeId { get; set; }
       // public string? DiscountCode { get; set; }

    }
}
