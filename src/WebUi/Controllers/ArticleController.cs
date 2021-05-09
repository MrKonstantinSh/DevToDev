using System.Threading.Tasks;
using DevToDev.Application.Article.Commands.CreateArticle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    public class ArticleController : ApiControllerBase
    {
        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<int>> GetCurrentUserInfo(CreateArticleCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}