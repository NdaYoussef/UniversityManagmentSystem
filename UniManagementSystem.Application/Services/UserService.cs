using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Application.DTOs;
using UniManagementSystem.Application.DTOs.UserDtos;
using UniManagementSystem.Application.Interfaces;
using UniManagementSystem.Domain.Models;
using UniManagementSystem.Infrastructure.DBContext;

namespace UniManagementSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly UniSystemContext _context;
        private readonly ICloudinaryService _cloudinaryService;
        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper, 
            UniSystemContext context, ICloudinaryService cloudinaryService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _cloudinaryService = cloudinaryService;
        }


        public async Task<AuthDto> GetUserData(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if(user is null)
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = " User not found"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            //string dashboardRoute = string.Empty;
            //if (roles.Contains("Admin")) dashboardRoute = "/Admin/Dashboard";
            //else if (roles.Contains("Lecturer")) dashboardRoute = "/Lecturer/Dashboard";
            //else if (roles.Contains("Student")) dashboardRoute = "/Student/Dashboard";
            //else dashboardRoute = "/Dashboard";


            string dashboardRoute = roles.FirstOrDefault()! switch
            {
                "Admin"=> "/Admin/Dashboard",
                "Lecturer" => "/Lecturer/Dashboard",
                "Student" => "/Student/Dashboard",
            };



            var dto = _mapper.Map<UserDashboardDto>(user);
            var data = new
            {
                user = dto,
                Roles = roles,
                DashboardRoute = dashboardRoute
            };
            return new AuthDto
            {
                IsAuthenticated = true,
                Message = "User data retrieved",
                Data = data
            };
        }

        public async Task<AuthDto> UpdateUserData(ApplicationUser user)
        {
            var currentUser = await _userManager.FindByIdAsync(user.Id);
            if (currentUser is null)
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "User not found"
                };
            }
            user.Adapt(currentUser);
            // _mapper.Map(user, currentUser);
            var result =   await _userManager.UpdateAsync(currentUser);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = errors
                };
            }

            var dto = _mapper.Map<UserDashboardDto>(currentUser);
            return new AuthDto
            {
                IsAuthenticated = true,
                Message = "User updated successfully",
                Data = dto
            };
        }
        public  async Task<(bool IsSuccess, string message)> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) 
                return (false,"User not found");

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
            }

          return (true,"Password changed successfully");

            }

        public async Task<AuthDto> ChangeProfilePictureAsync(string userId,  IFormFile ProfileImage)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser is null)
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "User not found"
                };
            var imageUrl = await _cloudinaryService.UploadImageAsync(ProfileImage);
            currentUser.ProfilePic = imageUrl;
            if (string.IsNullOrEmpty(imageUrl))
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "Image upload failed"
                };
            }

            var result = await _userManager.UpdateAsync(currentUser);
            if (!result.Succeeded)
            {
                var errors = string.Empty ;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = errors
                };
            }

            var dto = _mapper.Map<UserDashboardDto>(currentUser);
            return new AuthDto
            {
                IsAuthenticated = true,
                Message = "Profile picture updated successfully",
                Data = dto
            };
        }

        public async Task<AuthDto> DeleteUserData(string userId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser is null)
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "User not found"
                };

            var role = await _userManager.GetRolesAsync(currentUser);
            if (role.Contains("Admin"))
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "Cannot delete admin user"
                };

            }
            var result = await _userManager.DeleteAsync(currentUser);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = errors
                };
            }

            return new AuthDto
            {
                IsAuthenticated = true,
                Message = "User deleted successfully"
            };
        }

       
       
    }
}
