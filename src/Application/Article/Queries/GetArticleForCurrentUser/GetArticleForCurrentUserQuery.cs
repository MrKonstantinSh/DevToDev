using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Article.Dtos;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Article.Queries.GetArticleForCurrentUser
{
    [Authorize(Roles = "User")]
    public class GetArticleForCurrentUserQuery : IRequest<List<ArticleDto>>
    {
    }

    public class GetArticleForCurrentUserQueryHandler : IRequestHandler<GetArticleForCurrentUserQuery, List<ArticleDto>>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetArticleForCurrentUserQueryHandler(IAppDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;

            _currentUserService = currentUserService;
        }

        public async Task<List<ArticleDto>> Handle(GetArticleForCurrentUserQuery request,
            CancellationToken cancellationToken)
        {
            var listOfArticlesForCurrentUser = await _context.Articles
                .Where(a => a.User.Email == _currentUserService.Email)
                .ToListAsync(cancellationToken);

            // TODO: use automapper
            var listOfArticleDto = listOfArticlesForCurrentUser.Select(articleWithAuthor => new ArticleDto
                {
                    Id = articleWithAuthor.Id,
                    Author = _currentUserService.Username,
                    Title = articleWithAuthor.Title,
                    Description = articleWithAuthor.Description,
                    Content = articleWithAuthor.Content,
                    DateOfCreation = articleWithAuthor.DateOfCreation
                })
                .ToList();

            return await Task.FromResult(listOfArticleDto);
        }
    }
}