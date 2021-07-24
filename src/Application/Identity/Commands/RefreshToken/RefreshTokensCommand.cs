using System;
using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Identity.Dtos;
using MediatR;

namespace DevToDev.Application.Identity.Commands.RefreshToken
{
    public class RefreshTokensCommand : IRequest<AccessTokenDto>
    {
        public string Fingerprint { get; set; }
    }

    public class RefreshTokensCommandHandler : IRequestHandler<RefreshTokensCommand, AccessTokenDto>
    {
        private readonly IAppDbContext _context;
        private readonly ICookieService _cookieService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IIdentityService _identityService;

        public RefreshTokensCommandHandler(IAppDbContext context, ICookieService cookieService,
            IDateTimeService dateTimeService, IIdentityService identityService)
        {
            _context = context;

            _cookieService = cookieService;
            _dateTimeService = dateTimeService;
            _identityService = identityService;
        }

        public async Task<AccessTokenDto> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
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

            _context.RefreshSessions.Remove(currentRefreshSession);
            await _context.SaveChangesAsync(cancellationToken);

            if (currentRefreshSession.ExpiresIn < _dateTimeService.UtcNow
                || currentRefreshSession.Fingerprint != request.Fingerprint)
            {
                _cookieService.RemoveRefreshTokenCookie();
                throw new UnauthorizedAccessException();
            }

            var user = currentRefreshSession.User;
            var newRefreshSession = _identityService.CreateRefreshSessionForUser(user, request.Fingerprint);

            await _context.RefreshSessions.AddAsync(newRefreshSession, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            string newAccessToken = _identityService.CreateAccessTokenForUser(user);

            _cookieService.SetRefreshTokenCookie(newRefreshSession.RefreshToken, newRefreshSession.ExpiresIn);

            return new AccessTokenDto {AccessToken = newAccessToken};
        }
    }
}