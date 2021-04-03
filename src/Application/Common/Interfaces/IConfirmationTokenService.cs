namespace DevToDev.Application.Common.Interfaces
{
    public interface IConfirmationTokenService
    {
        public string GenerateToken(byte tokenLength);
    }
}