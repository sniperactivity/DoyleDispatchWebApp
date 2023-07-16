using DoyleDispatchWebApp.Data;
using DoyleDispatchWebApp.Models;
using DoyleDispatchWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Client> _userManager;
        private readonly SignInManager<Client> _signinManager;
        private readonly DataContext _context;
        public AccountController(UserManager<Client> userManager, SignInManager<Client> signInManager, DataContext context)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if(user != null)
            {
                //User found, now check password
                var passwordcheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordcheck)
                {
                    //Password correct, sign in
                    var result = await _signinManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                //Password is incorrect
                TempData["Error"] = "Wrong Credentials, Please Try Again";
                return View(loginViewModel);
            }
            //User not found
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginViewModel);
        }
        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This Email Address Has Already Been Used";
                return View(registerViewModel);
            }
            var newuser = new Client()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };
            var newuserresponse = await _userManager.CreateAsync(newuser, registerViewModel.Password);
            if (newuserresponse.Succeeded)
                await _userManager.AddToRoleAsync(newuser, UserRoles.User);
            else
            {
                ModelState.AddModelError("", "Failed To Register- Use a stronger pass");
            }
            return RedirectToAction("Index", "Dashboard");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
