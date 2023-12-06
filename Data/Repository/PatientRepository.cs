using Core.Dtos;
using Core.Dtos.BookingDtos;
using Core.Dtos.DoctorDtos;
using Core.Dtos.PatientDtos;
using Core.Models;
using Core.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


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
                    DateOfBirth = addPatientDto.DateOfBirth
                };
                var result = await _userManager.CreateAsync(NewPatient, addPatientDto.Password);
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public async Task<bool> CancelBooking(int BookingId)
        {
            Booking booking = await _context.Bookings.FirstOrDefaultAsync(x=>x.Id==BookingId &&  x.BookingType == Core.Consts.RequestType.Pending);
            if (booking != null)
            {
                booking.BookingType = Core.Consts.RequestType.Cancelled;
                return true;
            }
            return false;
        }

        public IEnumerable<GetBookingDetailsDto> GetAllBooking()
        {
            return _context.Bookings.Select(x => new GetBookingDetailsDto()
            {
                Day=x.Appointment.Day.ToString(),
                BookingStatus = x.BookingType.ToString(),
                Time =x.Appointment.Hours.Where(t=>t.Id==x.AppointmentHourId).Select(x=>x.Time).FirstOrDefault(),
                Price=x.Doctor.Price,
                DoctorName = x.Doctor.ApplicationUsers.UserName,
                SpecializationName = x.Doctor.Specialization.Name,
                Image = x.Doctor.ApplicationUsers.Image,
                /////////-------------------------------------------->FinalPrice
                //DiscountCoupon = x.Doctor.ApplicationUsers.DiscountCoupon.DiscountCode,

            });
        }

        public async Task<IEnumerable<GetAllDoctorsDto>> GetAllDoctors(int Page, int PageSize, string Search)
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

                Appointment = (List<GetAppointmentDto>)x.Appointments.Select(x => new GetAppointmentDto()
                {
                    Days = x.Day.ToString(),
                    Times = x.Hours.Select(x => x.Time).ToList(),

                }),
            })
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();


        }

        public async Task<bool> AddBooking(AddBookingDto addBookingDto)
        {
           bool isExist=_context.Bookings.Any(x => x.AppointmentId == addBookingDto.appointmentId && x.AppointmentHourId == addBookingDto.timeId);
            if (!isExist)
            {
                _context.Bookings.Add(new()
                {
                    BookingType = Core.Consts.RequestType.Pending,
                    DoctorId = addBookingDto.DoctorId,
                    AppointmentId = addBookingDto.appointmentId,
                    AppointmentHourId = addBookingDto.timeId,
                    ApplicationUserId = addBookingDto.PatientId,
                   

                    //public DiscountCoupon? DiscountCoupon { get; set; }
                });
              //  return true;
            }
            return false;
        }
    }
}
