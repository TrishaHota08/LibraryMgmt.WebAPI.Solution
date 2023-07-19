using LibraryMgmt.EFCore.Domain.Interfaces;
using LibraryMgmt.EFCore.Service.DTOs;
using LibraryMgmt.EFCore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserLoginJWTokenService _loginJWTokenService;
        
        public AuthController(IUserLoginJWTokenService loginJWTokenService)
        {
            _loginJWTokenService = loginJWTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCredDTO creds)
        {
            var response=await _loginJWTokenService.Login(creds);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return NotFound(response.Data);
            }
        }
    }
}