using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Common.Security;
using MediatR;

namespace DevToDev.Application.Article.Commands.CreateArticle
{
    [Authorize(Roles = "User")]
    public class CreateArticleCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }

    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, int>
    {
        private readonly IAppDbContext _context;
        private readonly IDateTimeService _dateTimeService;
        private readonly ICurrentUserService _currentUserService;

        public CreateArticleCommandHandler(IAppDbContext context, IDateTimeService dateTimeService,
            ICurrentUserService currentUserService)
        {
            _context = context;

            _dateTimeService = dateTimeService;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = new Domain.Entities.Article.Article
            {
                Title = request.Title,
                Description = request.Description,
                Content = request.Content,
                Url = Guid.NewGuid().ToString(),
                DateOfCreation = _dateTimeService.UtcNow,
                DateOfLastUpdate = _dateTimeService.UtcNow,
                User = _context.Users.FirstOrDefault(u => u.Email == _currentUserService.Email)
            };

            await _context.Articles.AddAsync(article, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return article.Id;
        }
    }
}