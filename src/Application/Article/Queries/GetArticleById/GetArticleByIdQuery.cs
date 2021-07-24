using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Article.Dtos;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Article.Queries.GetArticleById
{
    [Authorize(Roles = "User")]
    public class GetArticleByIdQuery : IRequest<ArticleDto>
    {
        public int Id { get; set; }
    }

    public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleDto>
    {
        private readonly IAppDbContext _context;

        public GetArticleByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ArticleDto> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var articleWithAuthor = await _context.Articles
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            var article = new ArticleDto
            {
                Id = request.Id,
                Author = articleWithAuthor.User.Username,
                Title = articleWithAuthor.Title,
                Description = articleWithAuthor.Description,
                Content = articleWithAuthor.Content,
                DateOfCreation = articleWithAuthor.DateOfCreation
            };

            return await Task.FromResult(article);
        }
    }
}