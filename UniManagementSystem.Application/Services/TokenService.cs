using Duende.IdentityModel.Client;
using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Application.Interfaces;
using UniManagementSystem.Domain.Models;

namespace UniManagementSystem.Application.Services
{
    internal class TokenService : ITokenService
    {
        public Task<string> GenerateToken(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
        public Domain.Models.RefreshToken GenerateRefreshToken()
        {
           var randomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            return new Domain.Models.RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedAt = DateTime.UtcNow,
            };

        }

        public Task RevokeRefreshToken(string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
