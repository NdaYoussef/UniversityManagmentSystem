using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Domain.Models;

namespace UniManagementSystem.Application.Interfaces
{
    internal interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
        RefreshToken GenerateRefreshToken();
        Task RevokeRefreshToken(string UserId);
    }
}
