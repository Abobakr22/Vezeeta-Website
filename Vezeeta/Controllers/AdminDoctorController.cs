using Core.Dtos.DoctorDtos;
using Core.Service;
using Data;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using MimeKit.Text;
using Core.Consts;
using Microsoft.AspNetCore.Authorization;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(AccountType.Admin))]
    public class AdminDoctorController : ControllerBase
    {
        //using dependency injection to inject iDoctorrepository

        private readonly IDoctorService _doctorService;
        private readonly ApplicationDbContext _context;
        
        public AdminDoctorController(IDoctorService doctorService, ApplicationDbContext context )
        {
            _doctorService = doctorService;
            _context = context;
            
        }

        
        private async Task SendWelcomeEmail(string toEmail)
        {
            var fromEmail = "Abobakrragab3@gmail.com"; 
            var subject = "Welcome to Vezeeta App";
            var body = "Thank you for joining our Vezeeta App. Your account has been successfully created.";

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(fromEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = "Account created Successfully ";
            email.Body = new TextPart(TextFormat.Html) { Text = " Hello from Vezeeta app " };

            using var stmp = new MailKit.Net.Smtp.SmtpClient();
            stmp.Connect("smtp.gmail.com", 587);
            stmp.Authenticate(fromEmail, "mwcwnhaxuryhclimsjsm");
            stmp.Send(email);
            stmp.Disconnect(true);   
        }
    



    [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorByID([FromRoute] int id)
        {
            var Doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (Doctor is not null)
            {
                return Ok(Doctor);
            }
            return Ok("Doctor Not Found ");
        }

        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var Doctors = await _doctorService.GetAllDoctorsAsync(1 , 10 , "Ah");
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
                    await _doctorService.AddDoctorAsync(doctor);
                    //An error occurred while Adding a new Doctor : A socket operation was attempted to an unreachable network.
                   // await SendWelcomeEmail(doctor.EmailAddress); 
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
                    await _doctorService.UpdateDoctorAsync(doctor);
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
            var DeletedDoctor = await _doctorService.GetByIdAsync(id);
            if (DeletedDoctor is not null)
            {
                await _doctorService.DeleteDoctorAsync(id);
                return Ok("Doctor Deleted Successfully");
            }
            return Ok(false);
        }

    }
}
