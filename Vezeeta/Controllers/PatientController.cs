using Core.Dtos.BookingDtos;
using Core.Dtos.PatientDtos;
using Core.Service;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ApplicationDbContext _context;
        public PatientController(IPatientService patientService, ApplicationDbContext context)
        {
            _patientService = patientService;
            _context = context;
        }

        [HttpGet("SearchAllDoctors")]
        public async Task<IActionResult> SearchAllDoctors()
        {
            var Doctors = await _patientService.GetAllDoctorsSearch(1, 5, "Ah");
            if (Doctors is not null)
            {
                return Ok(Doctors);
            }
            return Ok(false);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientByID([FromRoute] string id)
        {
            var patient = await _patientService.GetPatientById(id);
            if (patient is not null)
            {
                return Ok(patient);
            }
            return Ok("Patient Not Found ");
        }

        [HttpGet("GetAllPatients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var Patients = await _patientService.GetAllPatients(1, 5, "A");
            if (Patients is not null)
            {
                return Ok(Patients);
            }
            return Ok(false);
        }

        [HttpGet("GetAllBookings")]
        public IActionResult GetAllBookings()
        {
            var Bookings = _patientService.GetAllBooking();
            if (Bookings is not null)
            {
                return Ok(Bookings);
            }
            return Ok(false);
        }

        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddNewPatient([FromBody] AddPatientDto Patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _patientService.AddNewPatient(Patient);
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while Adding a new Patient : {ex.Message}");
            }
        }

        [HttpPost("AddBooking")]
        public async Task<IActionResult> AddNewBooking([FromBody] AddBookingDto booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _patientService.AddBooking(booking);

                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while Adding a new Booking : {ex.Message}");
            }
        }

        [HttpPatch("CancelBooking")]
        public async Task<IActionResult> CancelBooking([FromQuery] int id)
        {
            var CancelledBooking = await _context.Bookings.FindAsync(id);
            if (CancelledBooking is not null)
            {
                await _patientService.CancelBooking(id);
                return Ok("Booking Cancelled Successfully");
            }
            return Ok(false);
        }
    }
}