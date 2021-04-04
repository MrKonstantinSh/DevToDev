using System;
using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Common.Security;
using MediatR;

namespace DevToDev.Application.Identity.Commands.LogOut
{
    [Authorize(Roles = "User")]
    public class LogOutCommand : IRequest
    {
    }

    public class LogOutCommandHandler : IRequestHandler<LogOutCommand>
    {
        private readonly IAppDbContext _context;
        private readonly ICookieService _cookieService;
        private readonly IIdentityService _identityService;

        public LogOutCommandHandler(IAppDbContext context, ICookieService cookieService,
            IIdentityService identityService)
        {
            _context = context;

            _cookieService = cookieService;
            _identityService = identityService;
        }

        public async Task<Unit> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            string currentRefreshToken = _cookieService.GetRefreshTokenCookie();

            if (currentRefreshToken == null)
            {
                throw new UnauthorizedAccessException();
            }

            var currentRefreshSession = await _identityService
                .GetUserRefreshSessionWithRelatedEntities(currentRefreshToken);

            if (currentRefreshSession == null)
            {
                _cookieService.RemoveRefreshTokenCookie();
                throw new UnauthorizedAccessException();
            }

            _cookieService.RemoveRefreshTokenCookie();

            _context.RefreshSessions.Remove(currentRefreshSession);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}