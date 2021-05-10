using System.Collections.Generic;
using System.Threading.Tasks;
using DevToDev.Application.Article.Commands.CreateArticle;
using DevToDev.Application.Article.Commands.DeleteArticle;
using DevToDev.Application.Article.Commands.UpdateArticleCommand;
using DevToDev.Application.Article.Dtos;
using DevToDev.Application.Article.Queries.GetArticleById;
using DevToDev.Application.Article.Queries.GetArticleByKeyWords;
using DevToDev.Application.Article.Queries.GetArticleForCurrentUser;
using DevToDev.Application.Article.Queries.GetNewArticles;
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
        [HttpGet("search")]
        public async Task<ActionResult<List<ArticleDto>>> GetArticleByKeyWords([FromQuery] GetArticleByKeyWordsQuery query)
        {
            return await Mediator.Send(query);
        }

        [Authorize]
        [HttpGet("my")]
        public async Task<ActionResult<List<ArticleDto>>> GetCurrentUserArticles([FromQuery] GetArticleForCurrentUserQuery query)
        {
            return await Mediator.Send(query);
        }

        [Authorize]
        [HttpGet("latest")]
        public async Task<ActionResult<List<ArticleDto>>> GetLatestTenArticles([FromQuery] GetNewArticlesQuery query)
        {
            return await Mediator.Send(query);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<int>> CreateArticle(CreateArticleCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update(int id, UpdateArticleCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteArticleCommand { Id = id });

            return NoContent();
        }
    }
}