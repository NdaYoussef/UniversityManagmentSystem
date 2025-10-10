using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Application.DTOs;
using UniManagementSystem.Application.DTOs.UserDtos;
using UniManagementSystem.Domain.Models;

namespace UniManagementSystem.Application.Interfaces
{
    public interface IAccountServicecs
    {
        Task<ApplicationUser> GetCurrentUserAsync(string email);
        Task<AuthDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthDto> LoginAsync(LoginDto loginDto);
        Task<AuthDto> LogoutAsync(string email);
    }
}
