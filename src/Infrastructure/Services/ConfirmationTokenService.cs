using System;
using System.Security.Cryptography;
using DevToDev.Application.Common.Interfaces;

namespace DevToDev.Infrastructure.Services
{
    public class ConfirmationTokenService : IConfirmationTokenService
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

        public string GenerateToken(byte tokenLength)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();

            byte[] randomBytes = new byte[tokenLength/2];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            var random = new Random();

            string t = BitConverter.ToString(randomBytes);
            string r = t.Replace("-", string.Empty);

            int l = r.Length;

            return r;
        }
    }
}