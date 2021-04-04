using System.Threading.Tasks;
using DevToDev.Application.Identity.Commands.LogIn;
using DevToDev.Application.Identity.Commands.RefreshToken;
using DevToDev.Application.Identity.Commands.RegisterUser;
using DevToDev.Application.Identity.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    public class IdentityController : ApiControllerBase
    {
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
    }
}