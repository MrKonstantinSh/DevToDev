using System;

namespace DevToDev.Application.Common.Interfaces
{
    public interface ICookieService
    {
        public string GetRefreshTokenCookie();

        public void SetRefreshTokenCookie(string refreshToken, DateTime expires);

        public void RemoveRefreshTokenCookie();
    }
}