using Core.Account_Manager;
using Core.Models;
using Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IBaseRepository<ApplicationUser> _appUser;

        public AccountController(IBaseRepository<ApplicationUser> appUser)
        {
            _appUser = appUser;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto  loginDto)
        {
            if (loginDto == null)
                return BadRequest();

          bool result=  await _appUser.LoginAsync(loginDto);
            return Ok(result);
        }

    }
}
