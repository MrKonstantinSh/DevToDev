using System;

namespace DevToDev.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public string UserAgent { get; }
        public string IpAddress { get; }

        public void SetRefreshTokenCookie(string refreshToken, DateTime expires);
    }
}