using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Application.DTOs;
using UniManagementSystem.Application.DTOs.UserDtos;
using UniManagementSystem.Application.Interfaces;
using UniManagementSystem.Domain.Models;

namespace UniManagementSystem.Application.Services
{
    internal class AccountServicecs : IAccountServicecs
    {
        public Task<ApplicationUser> GetCurrentUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AuthDto> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<AuthDto> LogoutAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AuthDto> RegisterAsync(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
