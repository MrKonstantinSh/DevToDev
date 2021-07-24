using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Identity.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Identity.Commands.LogIn
{
    public class LogInCommand : IRequest<AccessTokenDto>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
        public string Fingerprint { get; set; }
    }

    public class LogInCommandHandler : IRequestHandler<LogInCommand, AccessTokenDto>
    {
        private readonly IAppDbContext _context;
        private readonly ICookieService _cookieService;
        private readonly IIdentityService _identityService;

        public LogInCommandHandler(IAppDbContext context, ICookieService cookieService,
            IIdentityService identityService)
        {
            _context = context;

            _cookieService = cookieService;
            _identityService = identityService;
        }

        public async Task<AccessTokenDto> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserWithRelatedEntitiesByUsername(request.UsernameOrEmail)
                       ?? await _identityService.GetUserWithRelatedEntitiesByEmail(request.UsernameOrEmail);

            var userRefreshSessions = await _context.RefreshSessions
                .Where(rs => rs.UserId == user.Id)
                .ToListAsync(cancellationToken);

            if (userRefreshSessions.Count >= 5)
            {
                _context.RefreshSessions.RemoveRange(userRefreshSessions);
            }

            var refreshSession = _identityService.CreateRefreshSessionForUser(user, request.Fingerprint);

            await _context.RefreshSessions.AddAsync(refreshSession, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            string accessToken = _identityService.CreateAccessTokenForUser(user);

            _cookieService.SetRefreshTokenCookie(refreshSession.RefreshToken, refreshSession.ExpiresIn);

            return new AccessTokenDto {AccessToken = accessToken};
        }
    }
}