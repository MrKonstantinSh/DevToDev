using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using DevToDev.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshSession> RefreshSessions { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(new CancellationToken());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}