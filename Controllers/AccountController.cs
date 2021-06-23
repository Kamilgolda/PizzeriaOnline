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
    /*! Klasa konrolera do zarządzania kontami */
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        /**
        * Konstruktor
        * @param signInManager manadżer mogowania
        * @param signInManager manadżer użytkownika
        * @param signInManager manadżer roli
        */
        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            //Create_Account();
        }

        /**
        * Task odpowiadający edycji profilu
        * @return Widok EditProfile.cshtml
        */
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        
        {
            User authUser = await _userManager.GetUserAsync(User);


            return View(authUser);
        }

        /**
        * Task odpowiadający edycji profilu
        * @param user użytkownik poddawany edycji
        * @return Widok EditProfile.cshtml
        */
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

        /**
        * Odpowiada logowaniu
        * @return widok login.cshtml
        */
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        /**
        * odpowiada tworzeniu konta administratora
        */
        public async void Create_Account()
        {
            User user = new()
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
                    Claim claim = new(ClaimTypes.Email, user.Email);
                    await _userManager.AddClaimAsync(user, claim);
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
    }
        /**
        * Task odpowiadający logowaniu
        * param loginModel model logowania
        * @return Po zalogowaniu widok Menu.cshtml kontrolera Home
        * @return Po niepoprawnej próbie logowania widok login.cshtml @see Login()
        */
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

        /**
        * Task odpowiadający wylogowywaniu
        * @return Przejście do akcji Index kontrolera Home
        */
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

         /**
        * Odpowiada rejestracji konta
        * @return widok Register.cshtml
        */
        public IActionResult Register()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

         /**
        * Task odpowiadający Rejestracji konta
        * @param registerModel model rejestracji
        * @return Przejście do akcji Index kontrolera Home
        */
        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterPost(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User user = new()
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
                        Claim claim = new(ClaimTypes.Email, user.Email);
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

         /**
        * Odpowiada zablokowaniu dostępu
        * @return widok AccessDenied.cshtml
        */
        public IActionResult AccessDenied()
        {
            return View();
        }

         /**
        * Task odpowiadający potwierdzeniu usuwania konta zalogowanego użytkownika
        * @return widok DeleteConfirm.cshtml
        */
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteConfirm()
        {
            User authUser = await _userManager.GetUserAsync(User);
            if (authUser == null)
            {
                return NotFound();
            }
            return View(authUser);
        }

         /**
        * Task odpowiadający potwierdzeniu usuwania konta użytkownika
        * @param user użytkownik 
        * @return widok Login.cshtml
        */
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

         /**
        * Task odpowiadający przejściu do panelu administratora
        * @return widok AdminPanel.cshtml
        */
        [Authorize(Roles ="Admin")]
        public IActionResult AdminPanel()
        {
            List<User> users = _userManager.Users.ToList();
            ViewBag.RolesV = _roleManager.Roles.ToList();
            return View(users);
        }
       
         /**
        * Task odpowiadający zmianie roli konta użytkownika
        * @param IdUser identyfikator użytkownika
        * @param status nazwa roli
        * @return widok AdminPanel.cshtml
        */
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus(string IdUser, string status)
        {
            if (IdUser == null || status == null) return NotFound();
            var user = await _userManager.FindByIdAsync(IdUser);
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles); 
            await _userManager.AddToRoleAsync(user, status);

            return RedirectToAction("AdminPanel", "Account");

        }
    }
}
