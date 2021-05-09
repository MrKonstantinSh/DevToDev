using System.Threading.Tasks;
using DevToDev.Application.Article.Commands.CreateArticle;
using DevToDev.Application.Article.Dtos;
using DevToDev.Application.Article.Queries.GetArticleById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    public class ArticleController : ApiControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<ArticleDto>> GetArticleById([FromQuery] GetArticleByIdQuery query)
        {
            return await Mediator.Send(query);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<int>> CreateArticle(CreateArticleCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}