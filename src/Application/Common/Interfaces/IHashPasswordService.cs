namespace DevToDev.Application.Common.Interfaces
{
    public interface IHashPasswordService
    {
        public string HashPassword(string stringToHashing);

        public bool VerifyPassword(string password, string passwordHash);
    }
}