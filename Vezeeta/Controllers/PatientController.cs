using Core.Dtos.BookingDtos;
using Core.Dtos.PatientDtos;
using Core.Repository;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ApplicationDbContext _context;
        public PatientController(IPatientRepository patientRepository, ApplicationDbContext context)
        {
            _patientRepository = patientRepository;
            _context = context;
        }

        [HttpGet("SearchAllDoctors")]
        public async Task<IActionResult> SearchAllDoctors()
        {
            var Doctors = await _patientRepository.GetAllDoctorsSearch(1, 5, "Ah");
            if (Doctors is not null)
            {
                return Ok(Doctors);
            }
            return Ok(false);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientByID([FromRoute] string id)
        {
            var patient = await _patientRepository.GetPatientById(id);
            if (patient is not null)
            {
                return Ok(patient);
            }
            return Ok("Patient Not Found ");
        }

        [HttpGet("GetAllPatients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var Patients = await _patientRepository.GetAllPatients(1, 5, "A");
            if (Patients is not null)
            {
                return Ok(Patients);
            }
            return Ok(false);
        }

        [HttpGet("GetAllBookings")]
        public IActionResult GetAllBookings()
        {
            var Bookings = _patientRepository.GetAllBooking();
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
                    await _patientRepository.AddNewPatient(Patient);
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
                    await _patientRepository.AddBooking(booking);

                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while Adding a new Booking : {ex.Message}");
            }
        }

        [HttpDelete("CancelBooking")]
        public async Task<IActionResult> CancelBooking([FromQuery] int id)
        {
            var CancelledBooking = await _context.Bookings.FindAsync(id);
            if (CancelledBooking is not null)
            {
                await _patientRepository.CancelBooking(id);
                return Ok("Booking Cancelled Successfully");
            }
            return Ok(false);
        }
    }
}