using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Domain.Models;

namespace UniManagementSystem.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
        Domain.Models.RefreshToken GenerateRefreshToken();
        Task RevokeRefreshToken(string UserId);
    }
}
