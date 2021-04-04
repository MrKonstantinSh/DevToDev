using System;
using DevToDev.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace WebUi.Services
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetRefreshTokenCookie()
        {
            return _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];
        }

        public void SetRefreshTokenCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Path = "api/identity",
                Expires = expires
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        public void RemoveRefreshTokenCookie()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Path = "api/identity",
                Expires = DateTimeOffset.UtcNow.AddMinutes(-1)
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", "", cookieOptions);
        }
    }
}