using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Article.Dtos;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Article.Queries.GetNewArticles
{
    [Authorize(Roles = "User")]
    public class GetNewArticlesQuery : IRequest<List<ArticleDto>>
    {
    }

    public class GetNewArticlesQueryHandler : IRequestHandler<GetNewArticlesQuery, List<ArticleDto>>
    {
        private readonly IAppDbContext _context;

        public GetNewArticlesQueryHandler(IAppDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
        }

        public async Task<List<ArticleDto>> Handle(GetNewArticlesQuery request,
            CancellationToken cancellationToken)
        {
            var listOfArticlesWithAuthors = await _context.Articles
                .Include(a => a.User)
                .OrderByDescending(a => a.DateOfCreation)
                .Take(10)
                .ToListAsync(cancellationToken);

            // TODO: use automapper
            var listOfArticleDto = listOfArticlesWithAuthors.Select(articleWithAuthor => new ArticleDto
                {
                    Id = articleWithAuthor.Id,
                    Author = articleWithAuthor.User.Username,
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