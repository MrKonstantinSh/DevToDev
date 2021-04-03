using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Application.Identity.Dtos;
using DevToDev.Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Identity.Commands.LogIn
{
    public class LogInCommand : IRequest<LogInResponseDto>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
        public string Fingerprint { get; set; }
    }

    public class LogInCommandHandler : IRequestHandler<LogInCommand, LogInResponseDto>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;
        private readonly ITokenService _tokenService;

        public LogInCommandHandler(IAppDbContext context, ITokenService tokenService,
            ICurrentUserService currentUserService, IDateTimeService dateTimeService)
        {
            _context = context;

            _tokenService = tokenService;
            _dateTimeService = dateTimeService;
            _currentUserService = currentUserService;
        }

        public async Task<LogInResponseDto> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                           .Include(u => u.UserDetails)
                           .Include(u => u.Roles)
                           .SingleOrDefaultAsync(u => u.Username == request.UsernameOrEmail, cancellationToken) ??
                       await _context.Users
                           .Include(u => u.UserDetails)
                           .Include(u => u.Roles)
                           .SingleOrDefaultAsync(u => u.Email == request.UsernameOrEmail, cancellationToken);

            var roles = new List<string>();
            roles.AddRange(user.Roles.Select(role => role.Name));

            string accessToken = _tokenService.GenerateAccessToken(user.Id, user.Username, user.Email,
                user.UserDetails.FirstName, user.UserDetails.LastName, roles.ToArray());
            string refreshToken = _tokenService.GenerateRefreshToken();

            var refreshSession = new RefreshSession
            {
                RefreshToken = refreshToken,
                UserAgent = _currentUserService.UserAgent,
                Fingerprint = request.Fingerprint,
                IpAddress = _currentUserService.IpAddress,
                CreatedAt = _dateTimeService.UtcNow,
                ExpiresIn = _dateTimeService.UtcNow.AddMonths(1),
                User = user
            };

            await _context.RefreshSessions.AddAsync(refreshSession, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _currentUserService.SetRefreshTokenCookie(refreshToken, refreshSession.ExpiresIn);

            return new LogInResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}