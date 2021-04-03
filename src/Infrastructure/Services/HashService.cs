using DevToDev.Application.Common.Interfaces;

namespace DevToDev.Infrastructure.Services
{
    public class HashService : IHashService
    {
        public string Hash(string stringToHashing)
        {
            return BCrypt.Net.BCrypt.HashPassword(stringToHashing);
        }
    }
}