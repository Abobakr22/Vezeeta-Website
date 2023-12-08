using Azure.Core;
using Core.Consts;
using Core.Dtos;
using Core.Dtos.BookingDtos;
using Core.Dtos.DoctorDtos;
using Core.Dtos.PatientDtos;
using Core.Models;
using Core.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing.Printing;


namespace Data.Repository
{
    public class PatientRepository : BaseRepository<ApplicationUser>, IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public PatientRepository(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager,
              UserManager<ApplicationUser> userManager) : base(context, signInManager, userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> AddNewPatient(AddPatientDto addPatientDto)
        {
            try
            {
                var NewPatient = new ApplicationUser
                {
                    Image = addPatientDto.Image,
                    FirstName = addPatientDto.FirstName,
                    LastName = addPatientDto.LastName,
                    UserName = addPatientDto.FirstName + addPatientDto.LastName,
                    Email = addPatientDto.Email,
                    PhoneNumber = addPatientDto.PhoneNumber,
                    Gender = addPatientDto.Gender,
                    DateOfBirth = addPatientDto.DateOfBirth,
                    AccountType = addPatientDto.AccountType

                };
                var result = await _userManager.CreateAsync(NewPatient, addPatientDto.Password);
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public async Task<IEnumerable<GetAllDoctorsDto>> GetAllDoctorsSearch(int Page, int PageSize, string Search)
        {
            return await _context.Doctors.Select(x => new GetAllDoctorsDto
            {
                Image = x.ApplicationUsers.Image,
                FirstName = x.ApplicationUsers.FirstName,
                LastName = x.ApplicationUsers.LastName,
                FullName = x.ApplicationUsers.FirstName + " " + x.ApplicationUsers.LastName,
                Email = x.ApplicationUsers.Email,
                Phone = x.ApplicationUsers.PhoneNumber,
                SpecializationName = x.Specialization.Name,
                Price = x.Price,
                Gender = x.ApplicationUsers.Gender.ToString(),

                Appointments = (List<GetAppointmentDto>)x.Appointments.Select(x => new GetAppointmentDto()
                {
                    Days = x.Day.ToString(),
                    Times = x.Hours.Select(x => x.Time).ToList(),

                }),
            })
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }

        public async Task<bool> CancelBooking(int BookingId)
        {
            Booking booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == BookingId && x.BookingType == Core.Consts.RequestType.Pending);
            if (booking != null)
            {
                booking.BookingType = Core.Consts.RequestType.Cancelled;
                return true;
            }
            return false;
        }

        public async Task<bool> AddBooking(AddBookingDto addBookingDto)
        {
           bool isExist=_context.Bookings.Any(x => x.AppointmentId == addBookingDto.appointmentId && x.AppointmentHourId == addBookingDto.timeId);
            if (!isExist)
            {
                try
                {
                    var booking = new Booking()
                    {
                        BookingType = RequestType.Pending,
                        DoctorId = addBookingDto.DoctorId,
                        ApplicationUserId = addBookingDto.PatientId,
                        AppointmentId = addBookingDto.appointmentId,
                        AppointmentHourId = addBookingDto.timeId
                    };
                    var result = await _context.Bookings.AddAsync(booking);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch(Exception ex)
                {
                    
                }               
            }
            return false;
        }

        public IEnumerable<GetBookingDetailsDto> GetAllBooking()
        {
            return _context.Bookings.Select(x => new GetBookingDetailsDto()
            {
                Day = x.Appointment.Day.ToString(),
                BookingStatus = x.BookingType.ToString(),
                Time = x.Appointment.Hours.Where(t => t.Id == x.AppointmentHourId).Select(x => x.Time).FirstOrDefault(),
                Price = x.Doctor.Price,
                DoctorName = x.Doctor.ApplicationUsers.UserName,
                SpecializationName = x.Doctor.Specialization.Name,
                Image = x.Doctor.ApplicationUsers.Image,
                DiscountCode = x.DiscountCoupon.DiscountCode,

                FinalPrice = x.DiscountCouponId.HasValue ?
                             (x.DiscountCoupon.DiscountType.Equals(DiscountType.Value) ?
                              x.Doctor.Price - x.DiscountCoupon.DiscountAmount :
                              x.Doctor.Price - ((x.DiscountCoupon.DiscountAmount / 100) * x.Doctor.Price)) : 
                              x.Doctor.Price
            }) ;
        }

        public async Task<GetPatientDto> GetPatientById(string PatientId)
        { 
            var Patient = await _context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == PatientId);
            if (Patient != null)
            {
                var details = new GetPatientDto
                {
                    Image = Patient.Image,
                    FullName = Patient.UserName,
                    Email = Patient.Email,
                    PhoneNumber = Patient.PhoneNumber,
                    Gender = Patient.Gender.ToString(),
                    DateOfBirth = Patient.DateOfBirth,

                    requests = _context.Bookings.Where(x => x.Patient.Id == PatientId).Select(x => new GetBookingDetailsDto()
                    {
                        Day = x.Appointment.Day.ToString(),
                        BookingStatus = x.BookingType.ToString(),
                        Time = x.Appointment.Hours.Where(t => t.Id == x.AppointmentHourId).Select(x => x.Time).FirstOrDefault(),
                        Price = x.Doctor.Price,
                        DoctorName = x.Doctor.ApplicationUsers.UserName,
                        SpecializationName = x.Doctor.Specialization.Name,
                        Image = x.Doctor.ApplicationUsers.Image,
                        DiscountCode = x.DiscountCoupon.DiscountCode,
                        FinalPrice = x.DiscountCouponId.HasValue ?
                            (x.DiscountCoupon.DiscountType.Equals(DiscountType.Value) ?
                             x.Doctor.Price - x.DiscountCoupon.DiscountAmount :
                             x.Doctor.Price - ((x.DiscountCoupon.DiscountAmount / 100) * x.Doctor.Price)) :
                             x.Doctor.Price
                    }).ToList()
                };
                    
                return details;      
            }
            else
            {
                throw new Exception("patient not found");
            }
        }

        public async Task<IEnumerable<GetAllPatientsDto>> GetAllPatients(int Page, int PageSize, string Search)
        {
            
            return await _context.ApplicationUsers.Where(x => x.AccountType == AccountType.Patient)
                .Select(x => new GetAllPatientsDto
            {
                Gender = x.Gender.ToString(),
                Image = x.Image,
                FullName = x.FirstName + " " + x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                DateOfBirth = x.DateOfBirth,
                AccountType = x.AccountType.ToString()
            })
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}















//FinalPrice = checkIfExist(x.Doctor.Price, x.DiscountCouponId),
//private static  double checkIfExist(double price,int? couponId)
//{
//    if (couponId.HasValue)
//    {
//        var coupon= _context.DiscountCoupons.FirstOrDefault(x => x.IsValid && x.Id == couponId.Value);
//        if (coupon != null) 
//        {
//            if (coupon.DiscountType.Equals(DiscountType.Value))
//                return price - coupon.DiscountAmount;

//            else
//                return price - ((coupon.DiscountAmount / 100) * price);
//        }
//    }
//    return 0;
//}

