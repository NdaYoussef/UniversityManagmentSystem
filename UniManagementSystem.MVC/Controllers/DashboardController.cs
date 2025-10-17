using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniManagementSystem.Application.Interfaces;

namespace UniManagementSystem.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        //get admin Dashboard 

        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }
            return userRole?.ToLower() switch
            {
                "admin" => RedirectToAction("Admin"),
                "lecturere" => RedirectToAction("Lecturer"),
                "student" =>RedirectToAction("Student"),
                _ => RedirectToAction("Unauthorized")
            };
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Admin()
        {
            var result = await _dashboardService.GetAdminDashboardData();
            if (!result.IsAuthenticated)
            {
                TempData["Error"] = result.Message;
                return View("Error");
            }

             return View("Admin", result.Data);
        }

        [Authorize(Roles = "Lecturer")]
        public async Task<IActionResult> Lecturer()
        {
            var lecturereId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _dashboardService.GetLecturerDashboardDataAsync(lecturereId);

            if (!result.IsAuthenticated)
            {
                TempData["Error"] = result.Message;
                return View("Error");
            }
            return View("LecturerDashboard", result.Data);  
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Student()
        {
            var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _dashboardService.GetStudentDashboardDataAsync(studentId);

            if(!result.IsAuthenticated)
            {
                TempData["Error"] = result.Message;
                return View("Error");
            }
            return View("StudentDashboard", result.Data);
        }

        public IActionResult Unauthorized()
        { return View(); }  


    }
}
