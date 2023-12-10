
using Core.Dtos.AppointmentDtos;
using Core.Models;
using Core.Service;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace Services
{
    public class AppointmentService : BaseRepository<Appointment>, IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AppointmentService(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager,
              UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(context, signInManager, userManager, roleManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> AddAppointment(AddApointmentDto addApointmentDto)
        {
            try
            {
                // Check if DoctorId exists
                if (!_context.Doctors.Any(d => d.Id == addApointmentDto.DoctorId))
                {
                    Console.WriteLine($"Doctor with ID {addApointmentDto.DoctorId} does not exist.");
                    return false;
                }
                var newAppointment = new Appointment
                {
                    Day = addApointmentDto.Days.FirstOrDefault(),
                    Hours = addApointmentDto.times.Select(time => new AppointmentHour { Time = TimeSpan.FromHours(time) }).ToList(),
                    DoctorId = addApointmentDto.DoctorId,
                };
                _context.Appointments.Add(newAppointment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding appointment: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAppointment(UpdateAppointmentDto updateAppointmentDto)
        {
            // Find an existing appointment
            var updatedAppointment = await _context.AppointmentHours
                                   .FirstOrDefaultAsync(z => z.Id == updateAppointmentDto.AppointmentId);
            if (updatedAppointment != null)
            {
                // Check if that time booked or not
                var isTimeAlreadyBooked = await _context.Bookings
                                        .AnyAsync(b => b.AppointmentHourId == updateAppointmentDto.AppointmentId);
                if (isTimeAlreadyBooked)
                {
                    // Time is already booked
                    return false;
                }
                updatedAppointment.Appointment.Day = updateAppointmentDto.Days.FirstOrDefault();
                updatedAppointment.Appointment.Hours = updateAppointmentDto.times.Select(time => new AppointmentHour { Time = TimeSpan.FromHours(time) }).ToList();

                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                // Appointment not found
                throw new Exception("Appointment not found");
            }
        }

        public async Task<bool> DeleteAppointment(int AppointmentHourId)
        {
            var appointment = await _context.AppointmentHours
                            .FirstOrDefaultAsync(x => x.Id == AppointmentHourId);

            if (appointment != null)
            {
                var isTimeAlreadyBooked = await _context.Bookings
                                        .AnyAsync(b => b.AppointmentHourId == AppointmentHourId);

                if (isTimeAlreadyBooked)
                {
                    return false;
                }
                _context.AppointmentHours.Remove(appointment);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new Exception("Appointment Not Found");
            }
        }

    }
}