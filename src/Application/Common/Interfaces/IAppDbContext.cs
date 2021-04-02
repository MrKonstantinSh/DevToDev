using System.Threading;
using System.Threading.Tasks;
using DevToDev.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshSession> RefreshSessions { get; set; }
        public DbSet<RevokedToken> RevokedTokens { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}