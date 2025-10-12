using Microsoft.AspNetCore.Http;
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
    public interface IUserDashboardService
    {
        Task<AuthDto> GetUserData(string userId);
        Task<AuthDto> UpdateUserData(ApplicationUser user);
        Task<AuthDto> DeleteUserData(string userId);
        Task<AuthDto> ChangeProfilePictureAsync(string userId,  IFormFile ProfileImage);

        Task<(bool IsSuccess, string message)> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
    }
}
