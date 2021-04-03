using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevToDev.Domain.Entities.Identity;

namespace DevToDev.Infrastructure.Persistence
{
    public static class AppDbContextSeed
    {
        public static async Task SeedRolesAsync(AppDbContext context)
        {
            if (!context.Roles.Any())
            {
                await context.Roles.AddRangeAsync(new List<Role>
                {
                    new()
                    {
                        Name = "Admin"
                    },
                    new()
                    {
                        Name = "User"
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}