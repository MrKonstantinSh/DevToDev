using System.Threading.Tasks;
using DevToDev.Application.Identity.Commands.RegisterUser;
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
    }
}