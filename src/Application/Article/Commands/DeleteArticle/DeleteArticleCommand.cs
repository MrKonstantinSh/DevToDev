using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Exceptions;
using DevToDev.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Article.Commands.DeleteArticle
{
    public class DeleteArticleCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteArticleCommandHandler(IAppDbContext context, ICurrentUserService cureCurrentUserService)
        {
            _context = context;

            _currentUserService = cureCurrentUserService;
        }

        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == request.Id
                && a.User.Email == _currentUserService.Email,
                cancellationToken);

            if (article == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Article.Article), request.Id);
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}