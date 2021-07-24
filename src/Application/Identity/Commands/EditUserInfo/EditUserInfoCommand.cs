using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Exceptions;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Common.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Identity.Commands.EditUserInfo
{
    [Authorize(Roles = "User")]
    public class EditUserInfoCommand : IRequest
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class EditUserInfoCommandHandler : IRequestHandler<EditUserInfoCommand>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public EditUserInfoCommandHandler(IAppDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;

            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(EditUserInfoCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Include(u => u.UserDetails)
                .FirstOrDefaultAsync(u => u.Email == _currentUserService.Email, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Article.Article), request.Username);
            }

            user.Username = request.Username;
            user.UserDetails.FirstName = request.FirstName;
            user.UserDetails.LastName = request.LastName;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}