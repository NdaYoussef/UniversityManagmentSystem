using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AccountServicecs(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor,
            SignInManager<ApplicationUser> signInManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public Task<ApplicationUser> GetCurrentUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthDto> LoginAsync(LoginDto loginDto)
        {
            var authModel = new AuthDto();
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                authModel.Message = "Email or Password is incorrect";
                return authModel;
            }
            //2- get user roles 
            var roles = await _userManager.GetRolesAsync(user);

            //3- login success
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if (result.Succeeded)
            {
                authModel.IsAuthenticated = true;
                authModel.Email = user.Email;
                authModel.UserName = user.UserName;
                authModel.Roles =new List<string> { user.Role.ToString() };
            }
            //4- check and generate refreshtoken
            if(user.RefreshTokens.Any(t=>t.IsActive))
            {
                var refreshToken = user.RefreshTokens.FirstOrDefault(t=>t.IsActive);
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;
            }
            else
            {
                var newRefreshToken = _tokenService.GenerateRefreshToken();
                authModel.RefreshToken = newRefreshToken.Token;
                authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
                user.RefreshTokens.Add(newRefreshToken);
                await _userManager.UpdateAsync(user);
            }
            return authModel;
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
