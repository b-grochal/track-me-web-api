using Microsoft.AspNetCore.Mvc;
using WebApi.Common;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class AuthController : ApiController
    {
        // This controller is responsible for handling authentication-related requests.
        // It can include actions like login, logout, register, etc.
        // For example:
        // [HttpPost("login")]
        // public async Task<IActionResult> Login(LoginCommand command)
        // {
        //     var result = await _mediator.Send(command);
        //     return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        // }
    }
}
