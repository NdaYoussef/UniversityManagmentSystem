﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniManagementSystem.Application.DTOs;
using UniManagementSystem.Application.DTOs.DashboardDtos;
using UniManagementSystem.Application.Interfaces;
using UniManagementSystem.Domain.Enums;
using UniManagementSystem.Domain.Models;
using UniManagementSystem.Infrastructure.DBContext;

namespace UniManagementSystem.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly UniSystemContext _context;
       // private readonly 
        public DashboardService(UniSystemContext context)
        {
            _context = context;
        }
        public async Task<AuthDto> GetAdminDashboardData()
        {
           var totalStudents = await _context.Users.CountAsync(u=>u.Role ==Roles.Student);
            var totalLecturers = await _context.Users.CountAsync(l => l.Role == Roles.Lecturer);
            var totalCourses = await _context.Courses.CountAsync();

            var totalDepartments = await _context.Departments.CountAsync();
            var totalFees = await _context.Fees.SumAsync(f=>f.Amount);

            var recentNotifications = await _context.Notifications
                                                    .OrderByDescending(n=>n.CreatedAt)
                                                    .Take(5)
                                                    .Select(n=> new
                                                    {
                                                        n.Message,
                                                        n.CreatedAt,                                
                                                    })
                                                    .ToListAsync();

            var AdminDashboardDto = new AdminDashboardDto
            {
                TotalStudents = totalStudents,
                TotalLecturers = totalLecturers,
                TotalCourses = totalCourses,
                TotalDepartments = totalDepartments,
                TotalFees = totalFees,
                RecentNotifications = (List<NotificationDto>)recentNotifications.Select(n => new
                {
                    n.Message,
                    n.CreatedAt
                }),
                SystemState = new SystemStateDto
                {
                    TotalUsers = totalStudents + totalLecturers,
                    // StoredUsage = await CalculateStorageUsage();
                    LastUpdated = DateTime.Now
                }
            };
            return new AuthDto
            {
                IsAuthenticated = true,
                Data = AdminDashboardDto,
                Message = "Admin dashboard data retrieved successfully",
            };


        }

        public async Task<AuthDto> GetLecturerDashboardDataAsync(string lecturerId)
        {

            var currentLecturer = await _context.Users.FirstOrDefaultAsync(l => l.Id == lecturerId && l.Role == Roles.Lecturer);
            if(currentLecturer == null)
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "Lecturer not found or unauthorized"
                };

            var totalCourses = await _context.UserCourses.Where(c => c.UserId == lecturerId)
                                                        .Select(c => new
                                                        {
                                                            Id = c.CourseId,
                                                            Name = c.Course.Name,
                                                            Code = c.Course.Code,
                                                            Department = c.Course.Department.Name,
                                                            MaxDegree = c.Course.MaxDegree,
                                                            MinDegree = c.Course.MinDegree,

                                                            StudentCount = c.Course.UserCourses.Count(),
                                                            UpcomingSchedules = c.Course.Schedules
                                                        })
                                                        .ToListAsync();
            var totalStudents = totalCourses.Sum(c => c.StudentCount);

            var LecturerDashboard = new 
            {
                LecturerName = $"{currentLecturer.FirstName} {currentLecturer.LastName}",
                TotalCourse = totalCourses.Count,
                TotalStudents = totalStudents,
                Courses = totalCourses

            };
            return new AuthDto
            {
                IsAuthenticated = true,
                Data = LecturerDashboard,
                Message = "Lecutruer dashboard data retrieved successfully",

            };

        }

        public async Task<AuthDto> GetStudentDashboardDataAsync(string studentId)
        {
            var currentStudent = await _context.Users.Include(d=>d.Department).FirstOrDefaultAsync(l => l.Id == studentId && l.Role == Roles.Student);
            if(currentStudent == null)
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "Student not found or unauthorized"
                };

            var courses = await _context.UserCourses.Where(s => s.UserId == studentId)
                .Select(s => new
                {
                    Id = s.CourseId,
                    Name = s.Course.Name,
                    Code = s.Course.Code,
                    Department = s.Course.Department.Name
                }).ToListAsync();

            var schedules = await _context.Schedules.Where(s => s.Course.UserCourses.Any(s => s.UserId == studentId))
                                                    .OrderBy(s => s.Day)
                                                    .ThenBy(s => s.StartTime)
                                                    .Take(5)
                                                    .Select(s => new
                                                    {
                                                        CourseName = s.Course.Name,
                                                        Day = s.Day.ToString(),
                                                        StartTime = s.StartTime.ToString(@"hh\:mm"),
                                                        EndTime = s.EndTime.ToString(@"hh\:mm"),
                                                        Lecturer = $"{s.Lecturer.FirstName} {s.Lecturer.LastName}"

                                                    }).ToListAsync();

            var studentDashboard = new
            {
                StudentInfo = new
                {
                    Name = $"{currentStudent.FirstName} {currentStudent.LastName}",
                    Email = currentStudent.Email,
                    Department = currentStudent.Department,
                    GPA = currentStudent.GPA ?? 0.0,
                    ProfilePic = currentStudent.ProfilePic,
                },
                TotalCourses = courses.Count(),
                Courses = courses,
                UpcomingSchedules = schedules.Count(),
                Schedules = schedules
            };
            return new AuthDto
            {
                IsAuthenticated = true,
                Data = studentDashboard,
                Message = "Student dashboard data retrieved successfully"
            };
        }

        //Task<List<NotificationDto>> IDashboardService.GetRecentNotificationsAsync(string userId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
