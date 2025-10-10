using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using UniManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using UniManagementSystem.Domain.Models;
using UniManagementSystem.Application.DTOs.UserDtos;



namespace UniManagementSystem.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServicecs _accountService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(IAccountServicecs accountServicecs, SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager)
        {
            _accountService = accountServicecs;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _accountService.RegisterAsync(model);
            TempData["SuccessMessage"] = "Registration successful!";
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveLogin(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =  await _accountService.LoginAsync(model);
            TempData["SuccessMessage"] = "Login successful!";
            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    await _accountService.LogoutAsync();
        //    return RedirectToAction("Login", "Account");
        //}

    }
}
