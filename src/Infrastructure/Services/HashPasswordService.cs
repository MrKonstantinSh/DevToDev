using DevToDev.Application.Common.Interfaces;

namespace DevToDev.Infrastructure.Services
{
    public class HashPasswordService : IHashPasswordService
    {
        public string HashPassword(string stringToHashing)
        {
            return BCrypt.Net.BCrypt.HashPassword(stringToHashing);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}