namespace DevToDev.Application.Common.Interfaces
{
    public interface ITokenService
    {
        public string GenerateAccessToken(int id, string username, string email,
            string firstName, string lastName, string[] roles);
    }
}