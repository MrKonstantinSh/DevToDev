using System.Threading.Tasks;
using DevToDev.Application.Identity.Commands.LogIn;
using DevToDev.Application.Identity.Commands.LogOut;
using DevToDev.Application.Identity.Commands.RefreshToken;
using DevToDev.Application.Identity.Commands.RegisterUser;
using DevToDev.Application.Identity.Dtos;
using DevToDev.Application.Identity.Queries.CheckEmailAddress;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    public class IdentityController : ApiControllerBase
    {
        [HttpGet("check-email")]
        public async Task<ActionResult<EmailStatusDto>> CheckEmailAddress([FromQuery] CheckEmailAddressQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("register")]
        public async Task<ActionResult<int>> RegisterUser(RegisterUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccessTokenDto>> LogIn(LogInCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize]
        [HttpPost("refresh-tokens")]
        public async Task<ActionResult<AccessTokenDto>> RefreshTokens(RefreshTokensCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> LogIn(LogOutCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}