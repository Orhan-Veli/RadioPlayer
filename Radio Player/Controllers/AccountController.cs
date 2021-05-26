using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Radio_Player.Authentication;
using Radio_Player.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Radio_Player.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return new JsonResult(false);

            var user = new ApplicationUser
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.UserName,
                Password = registerViewModel.Password
            };
            var role = await _roleManager.Roles.ToListAsync();

            if (role.Count == 0)
                return new JsonResult(false);

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return new JsonResult(true);
            }
            return new JsonResult(new { success = false, message = result.Errors.FirstOrDefault().Description });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return new JsonResult(false);           

                var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                if (user == null)
                    return new JsonResult(false);

                var password = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (password)
                {
                    var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, true, false);
                    if (result.Succeeded)                  
                        return new JsonResult(loginViewModel.UserName);

                return new JsonResult(false);                   
                }            
            return new JsonResult(false);                    
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return new JsonResult(true);

        }
    }
}
