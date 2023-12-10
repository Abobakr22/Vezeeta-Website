using Core.Consts;
using Core.Dtos.AppointmentDtos;
using Core.Service;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(AccountType.Doctor))]
    public class DoctorController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly ApplicationDbContext _context;
        public DoctorController(IAppointmentService appointmentService, ApplicationDbContext context, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _context = context;
            _doctorService = doctorService;
        }

        [HttpGet("GetAllBookingsOfDoctor")]
        public async Task<IActionResult> GetAllBookingsOfDoctor()
        {
            DateTime date = new DateTime(2023, 12, 20);  // Pass that date parameter to the method and change date as you want

            var AllBookings = await _doctorService.GetAllBookingsOfDoctor(date, 1, 5);
            if (AllBookings is not null)
            {
                return Ok(AllBookings);
            }
            return Ok(false);
        }

        [HttpPatch("ConfirmCheckUp")]
        public async Task<IActionResult> ConfirmCheckUp([FromQuery] int id)
        {
            var Booking = await _context.Bookings.FindAsync(id);
            if (Booking is not null)
            {
                await _doctorService.ConfirmCheckUp(id);
                return Ok("Request completed");
            }
            return Ok(false);
        }

        [HttpPost("AddAppointment")]
        public async Task<IActionResult> AddAppointment([FromBody] AddApointmentDto addApointmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _appointmentService.AddAppointment(addApointmentDto);
                    return Ok(result);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while Adding a new Patient : {ex.Message}");
            }
        }

        [HttpPatch("UpdateAppointment")]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentDto updateApointmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _appointmentService.UpdateAppointment(updateApointmentDto);
                    return Ok(result);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while Adding a new Appointment : {ex.Message}");
            }
        }

        [HttpDelete("DeleteAppointment")]
        public async Task<IActionResult> DeleteAppointment([FromQuery] int id)
        {
            var DeletedAppointment = await _appointmentService.GetByIdAsync(id);
            if (DeletedAppointment is not null)
            {
                var result = await _appointmentService.DeleteAppointment(id);
                return Ok(result);
            }
            return Ok(false);
        }

    }
}