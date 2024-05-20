using Doctors.Core.DTOs.AccountDto;
using Doctors.Core.Models;
using DoctorsNlayer.Helpers.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace DoctorsNlayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _role;

        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager,RoleManager<IdentityRole>role)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _role = role;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Register(RegisterDto registerDto)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            User user = new User()
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Surname=registerDto.Surname,
                UserName=registerDto.UserName,
            };
   var result =await _userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());
            //await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto,string? ReturnUrl)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.UsernameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "Username ve ya password yanlisdir ");
                    return View();
                }
            }
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "birazdan yeniden cehd edin");
                    return View();
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Username ve ya password yanlisdir ");
                    return View();
                }

            
            await _signInManager.SignInAsync(user, loginDto.isRemember);
            if (ReturnUrl == null)
            {
                return RedirectToAction(ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CreateRole()
        {
            foreach(var item in Enum.GetValues(typeof(UserRole)))
            {
                await _role.CreateAsync(new IdentityRole()
                {
                    Name = item.ToString()
                }) ;
            }
            return Ok();
        }
        public async Task<IActionResult> LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

