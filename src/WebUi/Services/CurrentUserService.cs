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

        public string Email => _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

        public string Username => _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value;

        public string FirstName => _httpContextAccessor.HttpContext?.User.FindFirst("firstname")?.Value;

        public string LastName => _httpContextAccessor.HttpContext?.User.FindFirst("lastname")?.Value;

        private string GetUserIpAddress()
        {
            if (_httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("X-Forwarded-For") != null
                && (bool) _httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("X-Forwarded-For"))
                return _httpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"];

            return _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }
    }
}