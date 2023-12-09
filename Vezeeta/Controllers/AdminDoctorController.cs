using Core.Dtos.DoctorDtos;
using Core.Repository;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDoctorController : ControllerBase
    {
        //using dependency injection to inject iDoctorrepository
        private readonly IDoctorRepository _doctorRepository;
        private readonly ApplicationDbContext _context;
        public AdminDoctorController(IDoctorRepository doctorRepository , ApplicationDbContext context)
        {
            _doctorRepository = doctorRepository;
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorByID([FromRoute] int id)
        {
            var Doctor = await _doctorRepository.GetDoctorByIdAsync(id);
            if (Doctor is not null)
            {
                return Ok(Doctor);
            }
            return Ok("Doctor Not Found ");
        }

        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var Doctors = await _doctorRepository.GetAllDoctorsAsync(1 , 10 , "Ah");
            if (Doctors is not null)
            {
                    return Ok(Doctors);             
            }
             return Ok(false);
        }

        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddNewDoctor([FromBody] AddDoctorDto doctor)
        {
            //{ image,firstName,lastName,email,phone,specialize,gender,dateOfBirth}
            try
            {
                if (ModelState.IsValid)
                {                    
                    await _doctorRepository.AddDoctorAsync(doctor);
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while Adding a new Doctor : {ex.Message}");

            }

        }
        [HttpPatch("UpdateDoctor")]
        public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorDto doctor)
        {     
            try
            {
                if (ModelState.IsValid)
                {
                    await _doctorRepository.UpdateDoctorAsync(doctor);
                    return Ok("Doctor Updated Successfully");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while Updating an Existing Doctor : {ex.Message}");

            }
        }

        [HttpDelete("DeleteDoctor")]
        public async Task<IActionResult> DeleteDoctor([FromQuery] int id)
        {
            var DeletedDoctor = await _doctorRepository.GetByIdAsync(id);
            if (DeletedDoctor is not null)
            {
                await _doctorRepository.DeleteDoctorAsync(id);
                return Ok("Doctor Deleted Successfully");
            }
            return Ok(false);
        }

    }
}
