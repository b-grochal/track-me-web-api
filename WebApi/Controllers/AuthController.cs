using Application.Authentication.Login;
using Application.Common.Messaging;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public class AuthController : ApiController
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand command, 
            [FromServices] ICommandHandler<LoginCommand, string> handler, 
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(command, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
