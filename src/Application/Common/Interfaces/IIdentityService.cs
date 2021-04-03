using System.Threading.Tasks;

namespace DevToDev.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        public Task<bool> UserIsInRoleAsync(int userId, string role);
    }
}