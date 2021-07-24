using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Article.Dtos;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Article.Queries.GetArticleByKeyWords
{
    [Authorize(Roles = "User")]
    public class GetArticleByKeyWordsQuery : IRequest<List<ArticleDto>>
    {
        public string SearchString { get; set; }
    }

    public class GetArticleByKeyWordsQueryHandler : IRequestHandler<GetArticleByKeyWordsQuery, List<ArticleDto>>
    {
        private readonly IAppDbContext _context;

        public GetArticleByKeyWordsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ArticleDto>> Handle(GetArticleByKeyWordsQuery request,
            CancellationToken cancellationToken)
        {
            string[] keyWords = request.SearchString.Split(' ');

            // TODO: change algorithm
            var listOfArticlesWithAuthors = await _context.Articles
                .Include(a => a.User)
                .ToListAsync(cancellationToken);

            listOfArticlesWithAuthors = listOfArticlesWithAuthors
                .Where(a => keyWords.Any(k => a.Content.Contains(k))
                    || keyWords.Any(k => a.Title.Contains(k))
                    || keyWords.Any(k => a.Description.Contains(k)))
                .ToList();

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