using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DevToDev.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DevToDev.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IDateTimeService _dateTimeService;

        public TokenService(IConfiguration configuration, IDateTimeService dateTimeService)
        {
            _configuration = configuration;
            _dateTimeService = dateTimeService;
        }

        public string GenerateAccessToken(int id, string username, string email,
            string firstName, string lastName, string[] roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_configuration["AccessToken:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", id.ToString()),
                    new Claim("username", username),
                    new Claim("email", email),
                    new Claim("firstName", firstName),
                    new Claim("lastName", lastName),
                    new Claim("roles", string.Join(",", roles))
                }),
                Issuer = _configuration["AccessToken:Issuer"],
                IssuedAt = _dateTimeService.UtcNow,
                Expires = _dateTimeService.UtcNow.AddMinutes(double.Parse(_configuration["AccessToken:ValidityTime"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return GenerateRandomString(int.Parse(_configuration["RefreshToken:Length"]));
        }

        public string GenerateConfirmationToken()
        {
            return GenerateRandomString(int.Parse(_configuration["ConfirmationToken:Length"]));
        }

        private static string GenerateRandomString(int length)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();

            byte[] randomBytes = new byte[length / 2];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", string.Empty);
        }
    }
}