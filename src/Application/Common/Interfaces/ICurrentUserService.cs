namespace DevToDev.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public string UserAgent { get; }
        public string IpAddress { get; }
        public string Email { get; }
        public string Username { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}