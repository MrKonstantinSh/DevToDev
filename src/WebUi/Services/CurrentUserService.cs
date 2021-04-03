using System;
using DevToDev.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace WebUi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserAgent => _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();

        public string IpAddress => _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.MapToIPv4().ToString();

        public void SetCookie(string key, string value, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value, cookieOptions);
        }
    }
}