using DevToDev.Application.Common.Interfaces;

namespace DevToDev.Infrastructure.Services
{
    // TODO: Change class and method naming.
    public class HashService : IHashService
    {
        public string Hash(string stringToHashing)
        {
            return BCrypt.Net.BCrypt.HashPassword(stringToHashing);
        }

        public bool Verify(string firstString, string secondString)
        {
            return BCrypt.Net.BCrypt.Verify(firstString, secondString);
        }
    }
}