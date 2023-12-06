using Core.Dtos.PatientDtos;
using Core.Repository;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        //using dependency injection to inject iDoctorrepository
        private readonly IPatientRepository _patientRepository;
        private readonly ApplicationDbContext _context;
        public PatientController(IPatientRepository patientRepository, ApplicationDbContext context)
        {
            _patientRepository = patientRepository;
            _context = context;
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
    }
}
