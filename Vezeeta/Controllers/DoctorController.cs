using Core.Dtos.AppointmentDtos;
using Core.Repository;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ApplicationDbContext _context;
        public DoctorController(IAppointmentRepository appointmentRepository, ApplicationDbContext context)
        {
            _appointmentRepository = appointmentRepository;
            _context = context;
        }

        [HttpPost("AddAppointment")]
        public async Task<IActionResult> AddAppointment([FromBody] AddApointmentDto addApointmentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _appointmentRepository.AddAppointment(addApointmentDto);
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
                    var result = await _appointmentRepository.UpdateAppointment(updateApointmentDto);
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
            var DeletedAppointment = await _appointmentRepository.GetByIdAsync(id);
            if (DeletedAppointment is not null)
            {
                var result = await _appointmentRepository.DeleteAppointment(id);
                return Ok(result);
            }
            return Ok(false);
        }

    }
}