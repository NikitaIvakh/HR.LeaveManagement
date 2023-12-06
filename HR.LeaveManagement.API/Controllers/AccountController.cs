using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest auth)
        {
            return Ok(await _authService.LoginAsync(auth));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponse>> Registration(RegistrationRequest registration)
        {
            return Ok(await _authService.RegisterAsync(registration));
        }
    }
}