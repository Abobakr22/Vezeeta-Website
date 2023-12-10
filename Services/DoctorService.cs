using Core.Consts;
using Core.Dtos;
using Core.Dtos.BookingDtos;
using Core.Dtos.DoctorDtos;
using Core.Dtos.StatisticsDtos;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Core.Service;
using System.Linq.Expressions;
using Data;

namespace Services
{
    public class DoctorService : BaseRepository<Doctor>, IDoctorService
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public DoctorService(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager,
              UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(context, signInManager, userManager, roleManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<GetDoctorDto> GetDoctorByIdAsync(int DoctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == DoctorId);
            if (doctor != null)
            {
                var details = new GetDoctorDto
                {
                    DoctorId = doctor.Id,
                    FirstName = doctor.ApplicationUsers.FirstName,
                    LastName = doctor.ApplicationUsers.LastName,
                    FullName = doctor.ApplicationUsers.UserName,
                    Email = doctor.ApplicationUsers.Email,
                    Phone = doctor.ApplicationUsers.PhoneNumber,
                    Image = doctor.ApplicationUsers.Image,
                    Gender = doctor.ApplicationUsers.Gender.ToString(),
                    DateOfBirth = doctor.ApplicationUsers.DateOfBirth,
                    SpecializationName = doctor.Specialization.Name,
                    price = doctor.Price
                };
                return details;
            }
            else
            {
                throw new Exception("Doctor not found");
            }
        }

        // [details:{image, fullName, email, phone, specialize, gender, dateOfBirth}]
        public async Task<IEnumerable<GetDoctorDto>> GetAllDoctorsAsync(int pageNumber, int pageSize, string Search)
        {
            return await _context.Doctors.Select(x => new GetDoctorDto
            {
                DoctorId = x.Id,
                Gender = x.ApplicationUsers.Gender.ToString(),
                Image = x.ApplicationUsers.Image,
                FirstName = x.ApplicationUsers.FirstName,
                LastName = x.ApplicationUsers.LastName,
                FullName = x.ApplicationUsers.FirstName + " " + x.ApplicationUsers.LastName,
                Email = x.ApplicationUsers.Email,
                Phone = x.ApplicationUsers.PhoneNumber,
                price = x.Price,
                SpecializationName = x.Specialization.Name,
                DateOfBirth = x.ApplicationUsers.DateOfBirth
            })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<bool> AddDoctorAsync(AddDoctorDto DoctorModel)
        {
            // {image,firstName,lastName,email,phone,Specialize,gender,dateOfBirth
            try
            {
                var NewDoctor = new ApplicationUser
                {
                    FirstName = DoctorModel.FirstName,
                    LastName = DoctorModel.LastName,
                    Image = DoctorModel.Image,
                    UserName = DoctorModel.FirstName + DoctorModel.LastName,
                    Email = DoctorModel.EmailAddress,
                    PhoneNumber = DoctorModel.PhoneNumber,
                    Gender = DoctorModel.Gender,
                    DateOfBirth = DoctorModel.DateOfBirth,
                    Doctor = new Doctor()
                    {
                        Id = DoctorModel.Id,
                        Price = DoctorModel.Price,
                        Specialization = new Specialization
                        {
                            Name = DoctorModel.Specialization.Name,
                        }
                    }
                };
                var result = await _userManager.CreateAsync(NewDoctor, DoctorModel.Password);
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        //{id,image,firstName,lastName,email,phone,specialize,gender,dateOfBirth
        public async Task<bool> UpdateDoctorAsync(UpdateDoctorDto DoctorModel)
        {
            var EditedDoctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == DoctorModel.Id);
            if (EditedDoctor != null)
            {
                EditedDoctor.ApplicationUsers.FirstName = DoctorModel.FirstName;
                EditedDoctor.ApplicationUsers.LastName = DoctorModel.LastName;
                EditedDoctor.ApplicationUsers.Image = DoctorModel.Image;
                EditedDoctor.ApplicationUsers.UserName = DoctorModel.FirstName + DoctorModel.LastName;
                EditedDoctor.ApplicationUsers.Email = DoctorModel.EmailAddress;
                EditedDoctor.ApplicationUsers.PhoneNumber = DoctorModel.PhoneNumber;
                EditedDoctor.ApplicationUsers.Gender = DoctorModel.Gender;
                EditedDoctor.ApplicationUsers.DateOfBirth = DoctorModel.DateOfBirth;
                EditedDoctor.Specialization.Name = DoctorModel.SpecializationName;

                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            else
            {
                throw new Exception("Doctor Not Found");
            }
        }

        public async Task<bool> DeleteDoctorAsync(int DoctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == DoctorId);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("You are not allowed to delete That Doctor");

            }
            return true;
        }

        public async Task<int> NumberOfDoctors()
        {
            var doctors = await _context.Doctors.CountAsync();
            if (doctors > 0)
            {
                return doctors;
            }
            return 0;
        }

        public List<TopSpecializationsDto> TopFiveSpecializations()
        {
            return _context.Doctors.Select(x => new TopSpecializationsDto
            {
                SpecializationName = x.Specialization.Name,
                TotalRequests = x.Requests.Count()
            })
            .OrderByDescending(z => z.TotalRequests)
            .Take(5)
            .ToList();
        }

        public List<TopDoctorsDto> TopTenDoctors()
        {
            return _context.Doctors.Select(x => new TopDoctorsDto
            {
                SpecializationName = x.Specialization.Name,
                Image = x.ApplicationUsers.Image,
                FullName = x.ApplicationUsers.FirstName + " " + x.ApplicationUsers.LastName,
                TotalRequests = x.Requests.Count()
            })
           .OrderByDescending(z => z.TotalRequests)
           .Take(10)
           .ToList();
        }

        public async Task<bool> ConfirmCheckUp(int BookingId)
        {
            Booking booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == BookingId && x.BookingType == RequestType.Pending);
            if (booking != null)
            {
                booking.BookingType = RequestType.Completed;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        //.Where(user => user.AccountType == AccountType.Patient)
        public async Task<IEnumerable<GetBookingDetailsDto>> GetAllBookingsOfDoctor(DateTime date, int pageSize, int pageNumber)
        {
            //var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == )
            var bookings = await _context.Bookings.Select(x => new GetBookingDetailsDto
            {
                Day = x.Appointment.Day.ToString(),
                Image = x.Doctor.ApplicationUsers.Image,
                Time = x.Appointment.Hours.Where(t => t.Id == x.AppointmentHourId).Select(x => x.Time).FirstOrDefault(),
                Age = CalculateAge(x.Patient.DateOfBirth),
                Patient = new ApplicationUser
                {
                    Gender = x.Patient.Gender,
                    PhoneNumber = x.Patient.PhoneNumber,
                    Email = x.Patient.Email,
                },
            })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return bookings;
        }

        private static int CalculateAge(DateTime BirthDay)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - BirthDay.Year;

            if (currentDate.Month < BirthDay.Month || currentDate.Month == BirthDay.Month && currentDate.Day < BirthDay.Day)
            {
                age--;
            }

            return age;
        }

        public Task<Doctor> FindAsync(Expression<Func<Doctor, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}