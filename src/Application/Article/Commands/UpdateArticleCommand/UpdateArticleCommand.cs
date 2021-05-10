using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Exceptions;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Article.Commands.UpdateArticleCommand
{
    [Authorize(Roles = "User")]
    public class UpdateArticleCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }

    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand>
    {
        private readonly IAppDbContext _context;
        private readonly IDateTimeService _dateTimeService;
        private readonly ICurrentUserService _currentUserService;

        public UpdateArticleCommandHandler(IAppDbContext context, IDateTimeService dateTimeService, ICurrentUserService currentUserService)
        {
            _context = context;

            _dateTimeService = dateTimeService;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == request.Id 
                && a.User.Email == _currentUserService.Email, 
                cancellationToken);

            if (article == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Article.Article), request.Id);
            }

            article.Title = request.Title;
            article.Description = request.Description;
            article.Content = request.Content;
            article.DateOfLastUpdate = _dateTimeService.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}