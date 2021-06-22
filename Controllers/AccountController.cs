using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzeriaOnline.Models;
using PizzeriaOnline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PizzeriaOnline.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            //Create_Account();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            User authUser = await _userManager.GetUserAsync(User);


            return View(authUser);
        }

        [HttpPost, ActionName("Edit")]
        [Authorize]
        public async Task<IActionResult> EditProfile(User user)
        {
            User authUser = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                if(user.FirstName != null)
                {
                    authUser.FirstName = user.FirstName;
                }
                if (user.LastName != null)
                {
                    authUser.LastName = user.LastName;
                }
                if (user.Email != null)
                {
                    authUser.Email = user.Email;
                }
                if (user.PhoneNumber != null)
                {
                    authUser.PhoneNumber = user.PhoneNumber;
                }
                if (user.Address != null)
                {
                    authUser.Address = user.Address;
                }
                var result = await _userManager.UpdateAsync(authUser);
                if (result.Succeeded)
                {
                    return RedirectToAction("EditProfile", "Account");
                }
            }
            ModelState.AddModelError("", "Failed to Login");
            return View(authUser);
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async void Create_Account()
        {
            User user = new User()
            {
                FirstName = "Admin",
                Address = "avc",
                LastName = "Admin",
                UserName = "Admin",
                PhoneNumber = "123",
                Email = "abc@gmail.com",
            };
            var result = await _userManager.CreateAsync(user, "zaq1@WSX");
            if (result.Succeeded)
            {
                bool roleExists = await _roleManager.RoleExistsAsync("Admin");
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                if (!await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }

                if (!string.IsNullOrWhiteSpace(user.Email))
                {
                    Claim claim = new Claim(ClaimTypes.Email, user.Email);
                    await _userManager.AddClaimAsync(user, claim);
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
    }

        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, loginModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Menu", "Home");
                }
            }
            ModelState.AddModelError("", "Błąd logowania");
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterPost(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    FirstName = registerModel.FirstName,
                    LastName = registerModel.LastName,
                    UserName = registerModel.UserName,
                    PhoneNumber = registerModel.PhoneNumber,
                    Email = registerModel.Email,
                    Address = registerModel.Adress
                };

                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    bool roleExists = await _roleManager.RoleExistsAsync("User");
                    if (!roleExists)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    }

                    if (!await _userManager.IsInRoleAsync(user, "User"))
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    if (!string.IsNullOrWhiteSpace(user.Email))
                    {
                        Claim claim = new Claim(ClaimTypes.Email, user.Email);
                        await _userManager.AddClaimAsync(user, claim);
                    }

                    var resultSignIn = await _signInManager.PasswordSignInAsync(registerModel.UserName, registerModel.Password, registerModel.RememberMe, false);
                    if (resultSignIn.Succeeded)
                    {
                        return RedirectToAction("Menu", "Home");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteConfirm()
        {
            User authUser = await _userManager.GetUserAsync(User);
            //var role = await _roleManager.FindByIdAsync(authUser.Id);
            //var role2 = (await _userManager.GetRolesAsync(authUser)).FirstOrDefault();
            if (authUser == null)
            {
                return NotFound();
            }
            return View(authUser);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteConfirm(User user)
        {
            User authUser = await _userManager.GetUserAsync(User);

            if (user == null || user.Id != authUser.Id)
            {
                return NotFound();
            }
            var userToDeleted = await _userManager.FindByIdAsync(user.Id);
            var result = await _userManager.DeleteAsync(userToDeleted);
            await _signInManager.SignOutAsync();

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles ="Admin")]
        public IActionResult AdminPanel()
        {
            List<User> users = _userManager.Users.ToList();
            
            return View(users);
        }
    }
}
