using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrackMe.BusinessServices.Interfaces;
using TrackMe.Models.DTOs.Auth;

namespace TrackMe.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusinessService authBusinessService;

        public AuthController(IAuthBusinessService authBusinessService)
        {
            this.authBusinessService = authBusinessService;
        }

        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var authenticatedUserDto = await authBusinessService.Authenticate(loginDto);
            return Ok(authenticatedUserDto);
        }

        public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
        {
            await authBusinessService.Register(registrationDto);
            return Ok();
        }
    }
}
