using System;
using System.Security.Claims;
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

        public string IpAddress => GetUserIpAddress();

        public string UserEmail => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

        private string GetUserIpAddress()
        {
            if (_httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("X-Forwarded-For") != null
                && (bool) _httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("X-Forwarded-For"))
                return _httpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"];

            return _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }
    }
}