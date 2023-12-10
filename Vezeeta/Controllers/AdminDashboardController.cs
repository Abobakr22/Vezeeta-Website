using Core.Repository;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        //using dependency injection to inject iDoctorrepository
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _petientRepository;
        private readonly ApplicationDbContext _context;
        public AdminDashboardController(IDoctorRepository doctorRepository, ApplicationDbContext context, IPatientRepository petientRepository)
        {
            _doctorRepository = doctorRepository;
            _context = context;
            _petientRepository = petientRepository;
        }

        [HttpGet("NumberOfDoctors")]
        public async Task<IActionResult> NumberOfDoctors()
        {
            var NumberOfDoctors = await _doctorRepository.NumberOfDoctors();
            if (NumberOfDoctors > 0)
            {
                return Ok(NumberOfDoctors);
            }
            return Ok(false);
        }

        [HttpGet("NumberOfPatients")]
        public async Task<IActionResult> NumberOfPatients()
        {
            var NumberOfPatients = await _petientRepository.NumberOfPatients();
            if (NumberOfPatients > 0)
            {
                return Ok(NumberOfPatients);
            }
            return Ok(false);
        }

        [HttpGet("NumberOfRequests")]
        public IActionResult NumberOfRequests()
        {
            var NumberOfRequests = _petientRepository.NumberOfRequests();
            return Ok(NumberOfRequests);
        }

        [HttpGet("TopFiveSpecializations")]
        public IActionResult TopFiveSpecializations()
        {
            var TopSpecializations = _doctorRepository.TopFiveSpecializations();
            if (TopFiveSpecializations != null)
            {
                return Ok(TopSpecializations);
            }
            return Ok(false);
        }

        [HttpGet("TopTenDoctors")]
        public IActionResult TopTenDoctors()
        {
            var TopTenDoctors = _doctorRepository.TopTenDoctors();
            if (TopTenDoctors != null)
            {
                return Ok(TopTenDoctors);
            }
            return Ok(false);
        }
    }
}