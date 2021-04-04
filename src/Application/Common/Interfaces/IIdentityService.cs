using System.Collections.Generic;
using System.Threading.Tasks;
using DevToDev.Domain.Entities.Identity;

namespace DevToDev.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        public Task<User> GetUserWithRelatedEntitiesByUsername(string username);

        public Task<User> GetUserWithRelatedEntitiesByEmail(string email);

        public List<string> GetUserRoles(User userWithRoles);

        public Task<bool> UserIsInRoleAsync(string email, string role);

        public Task<RefreshSession> GetUserRefreshSessionWithRelatedEntities(string refreshToken);

        public RefreshSession CreateRefreshSessionForUser(User user, string fingerprint);

        public string CreateAccessTokenForUser(User userWithRelatedEntities);
    }
}