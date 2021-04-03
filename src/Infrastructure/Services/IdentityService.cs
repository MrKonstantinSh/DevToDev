using System;
using System.Linq;
using System.Threading.Tasks;
using DevToDev.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevToDev.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IAppDbContext _context;

        public IdentityService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserIsInRoleAsync(int userId, string role)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.Id == userId);

            return user.Roles.Any(userRole => userRole.Name == role);
        }
    }
}