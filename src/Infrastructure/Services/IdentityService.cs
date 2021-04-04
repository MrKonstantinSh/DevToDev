using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevToDev.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IConfiguration _configuration;
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;
        private readonly ITokenService _tokenService;

        public IdentityService(IAppDbContext context, ITokenService tokenService,
            IConfiguration configuration, IDateTimeService dateTimeService,
            ICurrentUserService currentUserService)
        {
            _context = context;

            _tokenService = tokenService;
            _configuration = configuration;
            _dateTimeService = dateTimeService;
            _currentUserService = currentUserService;
        }

        public async Task<User> GetUserWithRelatedEntitiesByUsername(string username)
        {
            return await _context.Users
                .Include(u => u.UserDetails)
                .Include(u => u.Roles)
                .Include(u => u.RefreshSessions)
                .SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserWithRelatedEntitiesByEmail(string email)
        {
            return await _context.Users
                .Include(u => u.UserDetails)
                .Include(u => u.Roles)
                .Include(u => u.RefreshSessions)
                .SingleOrDefaultAsync(u => u.Email == email);
        }

        public List<string> GetUserRoles(User userWithRoles)
        {
            var roles = new List<string>();
            roles.AddRange(userWithRoles.Roles.Select(role => role.Name));

            return roles;
        }

        public async Task<bool> UserIsInRoleAsync(string email, string role)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.Email == email);

            return user.Roles.Any(userRole => userRole.Name == role);
        }

        public async Task<RefreshSession> GetUserRefreshSessionWithRelatedEntities(string refreshToken)
        {
            return await _context.RefreshSessions
                .Include(rs => rs.User)
                .ThenInclude(u => u.UserDetails)
                .Include(rs => rs.User)
                .ThenInclude(u => u.Roles)
                .SingleOrDefaultAsync(rs => rs.RefreshToken == refreshToken);
        }

        public RefreshSession CreateRefreshSessionForUser(User user, string fingerprint)
        {
            string refreshToken = _tokenService.GenerateRefreshToken();

            return new RefreshSession
            {
                RefreshToken = refreshToken,
                UserAgent = _currentUserService.UserAgent,
                Fingerprint = fingerprint,
                IpAddress = _currentUserService.IpAddress,
                CreatedAt = _dateTimeService.UtcNow,
                ExpiresIn = _dateTimeService.UtcNow.AddMinutes(double.Parse(_configuration["RefreshToken:ValidityTime"])),
                User = user
            };
        }

        public string CreateAccessTokenForUser(User userWithRelatedEntities)
        {
            string[] roles = GetUserRoles(userWithRelatedEntities).ToArray();

            return _tokenService.GenerateAccessToken(userWithRelatedEntities.Id,
                userWithRelatedEntities.Username, userWithRelatedEntities.Email,
                userWithRelatedEntities.UserDetails.FirstName,
                userWithRelatedEntities.UserDetails.LastName, roles);
        }
    }
}