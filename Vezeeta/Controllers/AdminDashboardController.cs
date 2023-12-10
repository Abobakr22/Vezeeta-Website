using Core.Service;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        //using dependency injection to inject iDoctorrepository
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly ApplicationDbContext _context;
        public AdminDashboardController(IDoctorService doctorService, ApplicationDbContext context, IPatientService patientService)
        {
            _doctorService = doctorService;
            _context = context;
            _patientService = patientService;
        }

        [HttpGet("NumberOfDoctors")]
        public async Task<IActionResult> NumberOfDoctors()
        {
            var NumberOfDoctors = await _doctorService.NumberOfDoctors();
            if (NumberOfDoctors > 0)
            {
                return Ok(NumberOfDoctors);
            }
            return Ok(false);
        }

        [HttpGet("NumberOfPatients")]
        public async Task<IActionResult> NumberOfPatients()
        {
            var NumberOfPatients = await _patientService.NumberOfPatients();
            if (NumberOfPatients > 0)
            {
                return Ok(NumberOfPatients);
            }
            return Ok(false);
        }

        [HttpGet("NumberOfRequests")]
        public IActionResult NumberOfRequests()
        {
            var NumberOfRequests = _patientService.NumberOfRequests();
            return Ok(NumberOfRequests);
        }

        [HttpGet("TopFiveSpecializations")]
        public IActionResult TopFiveSpecializations()
        {
            var TopSpecializations = _doctorService.TopFiveSpecializations();
            if (TopFiveSpecializations != null)
            {
                return Ok(TopSpecializations);
            }
            return Ok(false);
        }

        [HttpGet("TopTenDoctors")]
        public IActionResult TopTenDoctors()
        {
            var TopTenDoctors = _doctorService.TopTenDoctors();
            if (TopTenDoctors != null)
            {
                return Ok(TopTenDoctors);
            }
            return Ok(false);
        }
    }
}