using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevToDev.Application.Common.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace DevToDev.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IDateTimeService _dateTimeService;

        public TokenService(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        public string GenerateAccessToken(int id, string username, string email,
            string firstName, string lastName, string[] roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("E2CBA4F4-78AE-46DF-8579-EF6D651363C7");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", id.ToString()),
                    new Claim("username", username),
                    new Claim("email", email),
                    new Claim("firstName", firstName),
                    new Claim("lastName", lastName),
                    new Claim("roles", string.Join(",", roles)),
                }),
                Expires = _dateTimeService.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}